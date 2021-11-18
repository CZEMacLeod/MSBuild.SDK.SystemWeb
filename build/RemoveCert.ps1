Param(
   [string]$thumb
)
if ($thumb -ne "") {
	Add-Type -AssemblyName System.Security
	$store = new-object system.security.cryptography.X509Certificates.X509Store -argumentlist "MY", CurrentUser
	$store.Open([System.Security.Cryptography.X509Certificates.OpenFlags]"ReadWrite")
	$certs = $store.Certificates.Find([System.Security.Cryptography.X509Certificates.X509FindType]"FindByThumbprint", $thumb, $false)
	if ($certs -ne $null) {
		$store.RemoveRange($certs)
	}
	$store.Close()
}