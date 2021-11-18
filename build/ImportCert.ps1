$password = "$($env:password)"
$pfx64 = "$($env:pfx)"

$PfxUnprotectedBytes = [Convert]::FromBase64String($pfx64)

Add-Type -AssemblyName System.Security

$Pfx = New-Object System.Security.Cryptography.X509Certificates.X509Certificate2Collection

$Pfx.Import($PfxUnprotectedBytes, $null, [Security.Cryptography.X509Certificates.X509KeyStorageFlags]::Exportable)

$PfxProtectedBytes = $Pfx.Export([Security.Cryptography.X509Certificates.X509ContentType]::Pkcs12, $password)

$cert = New-Object System.Security.Cryptography.X509Certificates.X509Certificate2
$cert.Import($PfxProtectedBytes, $password, [System.Security.Cryptography.X509Certificates.X509KeyStorageFlags]"PersistKeySet")
$store = new-object system.security.cryptography.X509Certificates.X509Store -argumentlist "MY", CurrentUser
$store.Open([System.Security.Cryptography.X509Certificates.OpenFlags]"ReadWrite")
$store.Add($cert)
$store.Close()
Write-Host Imported certificate $cert
$thumb = $cert.Thumbprint
Write-Host ("##vso[task.setvariable variable=app_pfx_thumbprint;]$thumb")
Write-Host ("##vso[task.setvariable variable=MSBuildEmitSolution;]0")