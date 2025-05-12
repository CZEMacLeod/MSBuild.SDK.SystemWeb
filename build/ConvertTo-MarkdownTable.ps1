function ConvertTo-MarkdownTable {
    <#
    .SYNOPSIS
    Converts a collection of objects into a markdown-formatted table.

    .DESCRIPTION
    The `ConvertTo-MarkdownTable` function converts a collection of objects into a markdown-formatted table. The names
    of all properties on the first object are used as column names in the order they are defined. If subsequent objects
    define properties that were not present on the first item processed, those additional properties will be ignored
    and columns will not be created for them.

    Optionally, a maximum width can be specified for one, or all columns using MaxColumnWidth. However, if the length
    of the name column header is greater than the specified MaxColumnWidth, the MaxColumnWidth value used for that
    column will be the length of the column header. Rows with column values longer than MaxColumnWidth will be truncated
    and the Ellipsis string will be appended to the end with the length of the resulting string, plus ellipsis characters,
    equaling the MaxColumnWidth value for that column.

    By default, all columns will be padded with a space between any column header or value and the "|" characters on
    either side. Values shorter than the longest value in the column will be right-padded so that all "|" characters
    align vertically throughout the table.

    If the additional white space is not desired, use of the `Compress` switch will omit any unnecessary white space.

    .PARAMETER InputObject
    Specified the object, or a collection of objects to represent in the resulting markdown data table. All properties
    of InputObject will be used to define the resulting columns. Consider using `Select-Object` first to select which
    properties on the source object should be passed to this function.

    .PARAMETER MaxColumnWidth
    Specifies the maximum length of all columns if one value is provided, or the maximum length of each individual column
    if more than one value is provided. When providing more than one value, you must provide a value for every column. Columns
    with values longer than MaxColumnWidth will be truncated, and the Ellipsis characters will be appended. The length
    of the resulting string with ellipsis will match the MaxColumnWidth value.

    The default value is `[int]::MaxValue` so effectively no columns will be truncated. And the minimum value is the length
    of Ellipsis + 1, or 4 by default.

    .PARAMETER Ellipsis
    Specifies the characters to use as an ellipsis. By default, the ellipsis value is "...", but this can be overridden
    to be an empty string, or some other value. The minimum value for MaxColumnWidth is defined as 1 + the length of Ellipsis.

    .PARAMETER Compress
    Specifies that no extra padding should be added to make the "|" symbols align vertically.

    .EXAMPLE
    Get-Process | Select-Object Name, Id, VirtualMemorySize | ConvertTo-MarkdownTable -MaxColumnWidth 16

    Gets a list of processes, selects the Name, Id, and VirtualMemorySize properties, and returns a markdown-formatted
    table representing all properties with a maximum column width of 16 characters.

    .EXAMPLE
    Get-Service | Select-Object DisplayName, Name, Status | ConvertTo-MarkdownTable

    Generates a markdown-formatted table with the DisplayName, Name, and Status properties of all services.

    .EXAMPLE
    Get-Service | Select-Object DisplayName, Name, Status | ConvertTo-MarkdownTable -Compress

    Generates a markdown-formatted table with the DisplayName, Name, and Status properties of all services, without any
    unnecessary padding, resulting in a much shorter string for large sets of data.

    .NOTES
    Based on gist at https://gist.github.com/joshooaj/ef9b5ac769dd0f824c01497bf3e771dc
    #>#
    [CmdletBinding()]
    [OutputType([string])]
    param(
        [Parameter(Mandatory, ValueFromPipeline, Position = 0)]
        [psobject[]]
        $InputObject,

        [Parameter()]
        [ValidateRange(1, [int]::MaxValue)]
        [int[]]
        $MaxColumnWidth = ([int]::MaxValue),

        [Parameter()]
        [string]
        $Ellipsis = '...',

        [Parameter()]
        [switch]
        $Compress
    )

    begin {
        $MaxColumnWidth | ForEach-Object {
            if ($_ -le $Ellipsis.Length) {
                throw "MaxColumnWidth values must be greater than $($Ellipsis.Length) which is the length of the Ellipsis parameter. $_"
            }
        }
        $items = [system.collections.generic.list[object]]::new()
        $columns = [ordered]@{}
        $firstRecordProcessed = $false
    }

    process {
        foreach ($item in $InputObject) {
            $items.Add($item)
            $columnNumber = 0
            foreach ($property in $item.PSObject.Properties) {
                if ($MaxColumnWidth.Count -gt 1 -and $MaxColumnWidth.Count -lt ($columnNumber + 1)) {
                    throw "No MaxColumnWidth value defined for column $($columnNumber + 1). MaxColumnWidth must define a single value for all columns, or one value for each column."
                }

                $maxLength = $MaxColumnWidth[0]
                if ($MaxColumnWidth.Count -gt 1) {
                    $maxLength = $MaxColumnWidth[$columnNumber]
                }

                if (-not $columns.Contains($property.Name)) {
                    if ($firstRecordProcessed) {
                        Write-Warning "Ignoring property '$($property.Name)' on $item because the property was not present in the first item processed."
                        continue
                    } else {
                        $columns[$property.Name] = $property.Name.Length
                        if ($property.Name.Length -gt $maxLength) {
                            $maxLength = $property.Name.Length
                            Write-Warning "The header for column $columnNumber, '$($property.Name)', is longer than the MaxColumnWidth value provided. The MaxColumnWidth value for this column is now $maxLength."
                        }
                    }
                }

                $length = 0
                if ($null -ne $property.Value) {
                    $length = [math]::Min($maxLength, $property.Value.ToString().Length)
                }

                if ($columns[$property.Name] -lt $length) {
                    $columns[$property.Name] = $length
                }
                $columnNumber++
            }
            $firstRecordProcessed = $true
        }
    }

    end {
        function Shorten {
            param(
                [Parameter(ValueFromPipeline)]
                [string]
                $InputObject,

                [Parameter(Mandatory)]
                [ValidateRange(1, [int]::MaxValue)]
                [int]
                $MaxLength,

                [Parameter()]
                [string]
                $Ellipsis = '...'
            )

            process {
                if ($InputObject.Length -gt $MaxLength) {
                    '{0}{1}' -f $InputObject.Substring(0, ($MaxLength - $Ellipsis.Length)), $Ellipsis
                } else {
                    $InputObject
                }
            }
        }

        $sb = [text.stringbuilder]::new()

        # Header
        $paddedColumnNames = $columns.GetEnumerator() | ForEach-Object {
            $text = $_.Key | Shorten -MaxLength $_.Value -Ellipsis $Ellipsis
            if ($Compress) {
                ' {0} ' -f $text
            } else {
                ' {0} ' -f ($text.PadRight($_.Value))
            }
        }
        $null = $sb.AppendLine('|' + ($paddedColumnNames -join '|') + '|')
        $null = $sb.AppendLine('|' + (($paddedColumnNames | ForEach-Object { '-' * $_.Length } ) -join '|') + '|')

        foreach ($item in $items) {
            $paddedRowValues = $columns.GetEnumerator() | ForEach-Object {
                $text = [string]::Empty
                if ($null -ne $item.($_.Key)) {
                    $text = $item.($_.Key) | Shorten -MaxLength $_.Value -Ellipsis $Ellipsis
                }
                if ($Compress) {
                    ' {0} ' -f $text
                } else {
                    ' {0} ' -f $text.PadRight($_.Value)
                }
            }

            $null = $sb.AppendLine('|' + ($paddedRowValues -join '|') + '|')
        }
        $sb.ToString()
    }
}