# Teste de graceful shutdown

Além de utilizar o `IHostApplicationLifetime` para capturar quando a aplicação é finalizada, foi configurado `"console": "externalTerminal"` no arquivo `launch.json`. Outro ponto importante foi o ajuste no `ShutdownTimeout`.

Os pontos acima estão relacionados a aplicação, para utilização no kubernetes, avaliar o parâmetro `terminationGracePeriodSeconds`, para testar no docker usar `docker stop --time SEGUNDOS CONTAINER|ID`.

## Referências

https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.1

https://blog.markvincze.com/graceful-termination-in-kubernetes-with-asp-net-core/

https://cloud.google.com/blog/products/gcp/kubernetes-best-practices-terminating-with-grace

https://medium.com/better-programming/shut-down-docker-apps-gracefully-even-when-running-in-tmux-or-screen-41e68ff17187
