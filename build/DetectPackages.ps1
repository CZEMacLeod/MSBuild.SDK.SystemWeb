Import-Module .\build\ConvertTo-MarkdownTable.ps1
$dir = $env:BUILD_ARTIFACTSTAGINGDIRECTORY + "\*.nupkg"
Write-Host "Package Directory: $dir"
$packages = Get-ChildItem -Path $dir -Recurse
$ids = $packages | Select-Object -ExpandProperty name
$pkgs = @()
Write-Host "Source Branch Name: $env:BUILD_SOURCEBRANCHNAME"
Write-Host "Branch Name: $env:branchName"
$origin = "HEAD:$env:branchName"
ForEach ($id in $ids) {
	$names = $id.Split(".")
	$name = $names[(0..($names.Length-5))] -join "."
	$version = $names[(($names.Length-4)..($names.Length-2))] -join "."
	$pkg = New-Object -TypeName PSObject -Property @{
		Name = $name
		Version = $version
	}
	if ($env:branchName -eq "main") {
		$tag = "$($pkg.Name)_v$($pkg.Version)"
		Write-Host "Tagging Build: $tag"
		git tag $tag
	}
	$pkgs += $pkg
}
if ($env:branchName -eq "main") {
	git push origin --tags
}
$pkgs | Format-Table -Property Name, Version
Write-Host "Package Count: $($packages.Count)"
Write-Host ("##vso[task.setvariable variable=package_count;]$($packages.Count)")
$pushPackages = ($env:branchName -eq "main") -and ($packages.Count -gt 0)
Write-Host ("##vso[task.setvariable variable=push_packages;]$($pushPackages)")
$releaseNotes = $env:BUILD_ARTIFACTSTAGINGDIRECTORY + "\ReleaseNotes.md"
$header = "## Packages$([Environment]::NewLine)"
$header | Out-File $releaseNotes -Encoding ascii
$pkgs | ConvertTo-MarkdownTable | Add-Content $releaseNotes -Encoding ascii