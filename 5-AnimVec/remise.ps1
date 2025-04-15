try
{
  Install-Module powershell-yaml -Scope CurrentUser
  Import-Module powershell-yaml
  Push-Location $PSScriptRoot
  $teamFile = Get-Content "Assets\Exercice\Equipe.yaml"
  $team = ''
  foreach ($line in $teamFile) { $team = $team + "`n" + $line }
  $yaml = ConvertFrom-YAML $team
  if (-not ($yaml -is [Hashtable]) -or -not $yaml.ContainsKey('equipe'))
  {
    throw 'Equipe.yaml invalide'
  }
  if (-not ($yaml.equipe -is [Hashtable]) -or $yaml.equipe.Count -eq 0)
  {
    throw 'Equipe.yaml invalide'
  }
  Write-Host 'Coéquipiers:'
  foreach ($line in $yaml.equipe.GetEnumerator())
  {
    $code = $line.Name
    $name = $line.Value
    if (-not ($code -match '^[A-Z]{4}[0-9]{8}$'))
    {
      throw 'Code permanent invalide: ' + $code
    }
    Write-Host "- $($code): $name"
  }

  $sources = @("Assets\Exercice", "Assets\Exercice.meta")
  Compress-Archive -Path $sources -DestinationPath "remise.zip" -Force
  Pop-Location
}
catch
{
  Write-Error $_.Exception.ToString()
  Read-Host -Prompt "Il y a eu une erreur, appuyez sur Entrée pour quitter"  
}