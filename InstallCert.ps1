Param(
   [string]$pfxpath,
   [string]$password
)
Add-Type -AssemblyName System.Security
$cert = New-Object System.Security.Cryptography.X509Certificates.X509Certificate2
$cert.Import($pfxpath, $password, [System.Security.Cryptography.X509Certificates.X509KeyStorageFlags]"PersistKeySet")
$store = new-object system.security.cryptography.X509Certificates.X509Store -argumentlist "MY", CurrentUser
$store.Open([System.Security.Cryptography.X509Certificates.OpenFlags]"ReadWrite")
$store.Add($cert)
$store.Close()
Write-Host Imported certificate $cert
$thumb = $cert.Thumbprint
Write-Host ("##vso[task.setvariable variable=app_pfx_thumbprint;]$thumb")
Write-Host ("##vso[task.setvariable variable=MSBuildEmitSolution;]0")