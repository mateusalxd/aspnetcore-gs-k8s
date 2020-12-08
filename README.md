# Teste de graceful shutdown

Além de utilizar o `IHostApplicationLifetime` para capturar quando a aplicação é finalizada, foi configurado `"console": "externalTerminal"` no arquivo `launch.json`. Outro ponto importante foi o ajuste no `ShutdownTimeout`.

## Referências

https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.1

https://blog.markvincze.com/graceful-termination-in-kubernetes-with-asp-net-core/