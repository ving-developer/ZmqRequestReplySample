<p align="center"></p>
<p align="center">
  <span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;🡇</span>
  <br>
  <a href="../README.md">English 🇬🇧</a>
  ·
  <strong><a href="#">Português  🇧🇷</a></strong>
</p>

<br>
<br>

<div align="center">
  <img width="200" alt="zeromq logo" src=".\assets\zeromq.png" href="https://zguide.zeromq.org/docs/preface/"/>
  <br>
  <br>
</div>

<div style="display: inline_block" align="center">
  <img src="https://img.shields.io/github/last-commit/ving-developer/ZmqRequestReplySample?style=flat&logo=github"/>
  <img src="https://img.shields.io/github/stars/ving-developer?logo=github&color=yellow"/>
  <a href="https://www.linkedin.com/in/henrique-barros-7b1812209/">
    <img src="https://img.shields.io/badge/Linkedin-Henrique%20Barros-blue?style=flat&logo=linkedin"/>
  </a>
</div>

<div style="display: inline_block" align="center">
<img alt="ZeroMQ version" src="https://img.shields.io/nuget/v/zeromq?logo=zeromq&logoColor=%23f00&label=ZMQ&color=red&link=https%3A%2F%2Fzguide.zeromq.org%2Fdocs%2Fpreface%2F">
<img alt="dotnet 6" src="https://img.shields.io/badge/-.NET%206.0-blueviolet">
</div>

## O que é o [ØMQ](https://zguide.zeromq.org/docs/preface/)?

 __ZeroMQ__, ou __ØMQ__, ou até mesmo __ZMQ__, é uma ferramenta voltada para comunicação asíncrona entre servidores. Ele oferece várias abordagens para comunicação via __socket__ utilizando [unicast](https://zguide.zeromq.org/docs/chapter2/#Unicast-Transports) ou [multicast](https://zguide.zeromq.org/docs/chapter5/).

 Sua implementação consiste em um sistema de mensagens muito leve, especialmente projetado para cenários de alto rendimento/baixa latência. Além disso, __ØMQ__ é totalmente desacoplado podendo implementar qualquer protocolo de mensageria, ao contrário de outras tecnologias como __RabbitMQ__, que nativamente implementa o __AMQP__.

<div align="center">
  <img width="800" src=".\assets\amqp-example.png"/>
  <br>
  <br>
</div>

Porém, ao se utilizar o __ØMQ__, a tratativa de cenários avançados como roteamento, balanceamento de carga, enfileiramento de mensagens e persistência de mensagens em caso de falhas ou reinicializações devem ser configuradas no próprio projeto.

No final das contas, o __ØMQ__ é mais leve pois possui muito menos implementações nativas. Mas também oferece total controle sobre o fluxo da sua aplicação, sendo interessante para casos de extrema complexidade e também casos onde o mínimo latência fará muita diferença.


## Padrões de mensageria suportados

No __ØMQ__ existem vários tipos __sockets__, com as seguintes abordagens:

- [inproc](http://api.zeromq.org/master:zmq-inproc) - utilizando a função `zmq_inproc()` para transportar mensagens via memória diretamente entre threads que compartilham um único contexto __ØMQ__. Pode ser utilizado em padrões [__Exclusive-pair__](https://zguide.zeromq.org/docs/chapter2/#Messaging-Patterns), conforme mostra a própria documentação.
  
- [ipc](http://api.zeromq.org/master:zmq-ipc) - utiliza `zmq_ipc()` para transportar mensagens entre processos locais usando um mecanismo IPC dependente do sistema. Pode ser utilizado para se implementar um serviço de [__Pipeline__](https://zguide.zeromq.org/docs/chapter2/#Messaging-Patterns) conforme mostra a documentação. Atualmente só funciona em servidores UNIX.
  
- [tcp](http://api.zeromq.org/master:zmq-tcp) - utiliza `zmq_tcp()` para estabelecer uma conexão remota assíncrona entre servidores. É amplamente utilizado em [__Request-Reply__](https://zguide.zeromq.org/docs/chapter2/#Messaging-Patterns), onde tráfego de dados não será feito com __HTTP__. Mas sim utilizar o próprio padrão __ØMQ__ com um comprimento especificado e a mensagem. A imagem abaixo ilustra como os dados são passados através do __socket__.
  
<div align="center">
  <img width="200" src=".\assets\zmq-protocol.png"/>
  <br>
  <br>
</div>

- [pgm](http://api.zeromq.org/master:zmq-pgm) - utiliza `zmq_pgm()` para implementar uma estratégia de multicast, onde frames de dados são enviados através de um conjunto de endereços IP. Pode ser utilizado em padrões [__Pub-sub__](https://zguide.zeromq.org/docs/chapter2/#Messaging-Patterns) conforme mostra a documentação.

- [epgm](http://api.zeromq.org/master:zmq-epgm) - pode ser ativado através de `zmq_epgm()`, é semelhante ao pgm normal, mas aqui os frames de dados trafegam via UDP. Pode ser utilizado em padrões [__Pub-sub__](https://zguide.zeromq.org/docs/chapter2/#Messaging-Patterns) conforme mostra a documentação.


## Request-Reply


