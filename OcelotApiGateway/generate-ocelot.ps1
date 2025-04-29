$ConfigDir = ".\Config"
$OutputFile = "ocelot-combined.json"
$BaseUrl = "http://localhost:5000"

if (-Not (Test-Path $ConfigDir)) {
    Write-Host "❌ Diretório '$ConfigDir' não encontrado."
    exit 1
}

$allRoutes = @()

# Lê todas as rotas dos arquivos JSON
Get-ChildItem -Path $ConfigDir -Filter *.json | ForEach-Object {
    $json = Get-Content $_.FullName | ConvertFrom-Json
    if ($json.Routes) {
        $allRoutes += $json.Routes
    }
}

# Monta o objeto final
$finalConfig = @{
    GlobalConfiguration = @{
        BaseUrl = $BaseUrl
    }
    Routes = $allRoutes
}

# Converte para JSON formatado e salva
$finalConfig | ConvertTo-Json -Depth 10 | Out-File -Encoding UTF8 $OutputFile

Write-Host "✅ Arquivo '$OutputFile' gerado com sucesso!"
