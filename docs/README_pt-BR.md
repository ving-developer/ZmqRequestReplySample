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

<br>
<br>

# Sumário
1. [__Sobre o ØMQ__](https://github.com/ving-developer/ZmqRequestReplySample#what-is-%C3%B8mq)
1. [__Formato de comunicação__](https://github.com/ving-developer/ZmqRequestReplySample#what-is-%C3%B8mq)
1. [__Implementação Request-Reply__](https://github.com/ving-developer/ZmqRequestReplySample#what-is-%C3%B8mq)
1. [__Instalação__](https://github.com/ving-developer/ZmqRequestReplySample#what-is-%C3%B8mq)
1. [__Uso__](https://github.com/ving-developer/ZmqRequestReplySample#what-is-%C3%B8mq)


## Sobre o [ØMQ](https://zguide.zeromq.org/docs/preface/)?

 __ZeroMQ__, ou __ØMQ__, ou até mesmo __ZMQ__, é uma ferramenta voltada para comunicação asíncrona entre servidores. Ele oferece várias abordagens para comunicação via __socket__ utilizando [unicast](https://zguide.zeromq.org/docs/chapter2/#Unicast-Transports) ou [multicast](https://zguide.zeromq.org/docs/chapter5/).

 Sua implementação consiste em um sistema de mensagens muito leve, especialmente projetado para cenários de alto rendimento/baixa latência. Além disso, __ØMQ__ é totalmente desacoplado podendo implementar qualquer protocolo de mensageria, ao contrário de outras tecnologias como __RabbitMQ__, que nativamente implementa o __AMQP__.

<div align="center">
  <img width="800" src=".\assets\amqp-example.png"/>
  <br>
  <br>
</div>

Porém, ao se utilizar o __ØMQ__, a tratativa de cenários avançados como roteamento, balanceamento de carga, enfileiramento de mensagens e persistência de mensagens em caso de falhas ou reinicializações devem ser configuradas no próprio projeto.

No final das contas, o __ØMQ__ é mais leve pois possui muito menos implementações nativas. Mas também oferece total controle sobre o fluxo da sua aplicação, sendo interessante para casos de extrema complexidade e também casos onde o mínimo latência fará muita diferença.


## Formato de comunicação

No __ØMQ__, as mensagens são divididas em multiplas partes, onde cada parte é chamada de _frame_. Isto servirá para adicionar "cabeçalhos" em cada _frame_. O próprio zmq irá encarregar de entregar todos os _frames_ ou nenhum deles.

<div align="center">
  <img width="200" src=".\assets\zmq-protocol.png"/>
  <br>
  <br>
</div>

No __ØMQ__, os tipos de __sockets__ existentes são:

- [inproc](http://api.zeromq.org/master:zmq-inproc) - utilizando a função `zmq_inproc()` para transportar mensagens via memória diretamente entre threads que compartilham um único contexto __ØMQ__. Pode ser utilizado em padrões [__Exclusive-pair__](https://zguide.zeromq.org/docs/chapter2/#Messaging-Patterns), conforme mostra a própria documentação.
  
- [ipc](http://api.zeromq.org/master:zmq-ipc) - utiliza `zmq_ipc()` para transportar mensagens entre processos locais usando um mecanismo IPC dependente do sistema. Pode ser utilizado para se implementar um serviço de [__Pipeline__](https://zguide.zeromq.org/docs/chapter2/#Messaging-Patterns) conforme mostra a documentação. Atualmente só funciona em servidores UNIX.
  
- [tcp](http://api.zeromq.org/master:zmq-tcp) - utiliza `zmq_tcp()` para estabelecer uma conexão remota assíncrona entre servidores. É amplamente utilizado em [__Request-Reply__](https://zguide.zeromq.org/docs/chapter2/#Messaging-Patterns), onde tráfego de dados não será feito com __HTTP__. Mas sim utilizar o próprio padrão __ØMQ__ com um comprimento especificado e a mensagem. A imagem abaixo ilustra como os dados são passados através do __socket__.

- [pgm](http://api.zeromq.org/master:zmq-pgm) - utiliza `zmq_pgm()` para implementar uma estratégia de multicast, onde frames de dados são enviados através de um conjunto de endereços IP. Pode ser utilizado em padrões [__Pub-sub__](https://zguide.zeromq.org/docs/chapter2/#Messaging-Patterns) conforme mostra a documentação.

- [epgm](http://api.zeromq.org/master:zmq-epgm) - pode ser ativado através de `zmq_epgm()`, é semelhante ao pgm normal, mas aqui os frames de dados trafegam via UDP. Pode ser utilizado em padrões [__Pub-sub__](https://zguide.zeromq.org/docs/chapter2/#Messaging-Patterns) conforme mostra a documentação.


## Implementação Request-Reply

Se trata de um protocolo de comunicação onde o cliente faz uma requisição ao servidor, aguarda por uma resposta enquanto o servidor processa a requisição, e por fim, retorna uma resposta ao cliente. Para implementar este caso de uso, o [__ØMQ__](https://zguide.zeromq.org/) oferece um enpacotamento que separa a carga útil da mensagem, de um envelope.

No envelope irá conter o endereço de retorno das respostas. É assim que mesmo o __ØMQ__ não guardando um estado, pode implementar aplicações de solicitação-resposta. A imagem abaixo, ilustra uma requisição recebida pelo servidor, onde o _frame 1_ é o endereço do cliente que fez a solicitação. Podendo capturar este endereço e enviar uma resposta à ele.

<div align="center">
  <img width="600" src=".\assets\zmq-frames.jpg"/>
  <br>
  <br>
</div>

Aqui neste repositório, será exemplificada uma aplicação onde um cliente solicita uma pagina HTML para ser convertida em PDF, um enfileirador (broker) recebe a requisição e envia ela para um servidor; que por sua vez conveerte a página em PDF e devolve os bytes do PDF. Após isso o enfileirador repassa os bytes para o cliente e ele disponibiliza o PDF para download.

A imagem abaixo ilustra a estrutura dos projetos neste repositório.

<div align="center">
  <img width="300" src=".\assets\request-reply-sample.png"/>
  <br>
  <br>
</div>

Repare que utilizamos um __Router__ que recebe/envia dados do frontend e um __Dealer__ que recebe/envia dados do backend. Esta estrutura é feita, para desacoplar os clientes dos servidores. Onde caso o número de servidores aumente, não será preciso alterar o código dos N clientes para poder refletir as alterações. Somente o código do __Broker__ seria alterado para acrescentar os novos clientes e servidores.

## Instalação

Para utilizar __ØMQ__ em sua aplicação .NET, inicialmente será preciso instalar a bilbioteca _libzmq_ em seu projeto. Existem duas formas de fazer isso:

-  Instalando a biblioteca [NetMQ](https://www.nuget.org/packages/NetMQ/) em seu projeto. Para isto, abra sua solução e digite `Ctrl` + `` ` ``, este comando irá abrir o prompt na pasta do seu projeto. Apoós isto digite `dotnet add package ntemq` e pronto. Aqui, será instalado uma implementação 100% adaptada em C# do __ØMQ__ em sua aplicação.

- Instalando a biblioteca [ZeroMQ](https://www.nuget.org/packages/ZeroMQ). Para isto, abra sua solução e digite `Ctrl` + `` ` ``, este comando irá abrir o prompt na pasta do seu projeto. Apoós isto digite `dotnet add package zeromq`. Aqui, será instalada a biblioteca nativa _libzmq_ escrita em C++, com uma interface em C# para você utilizar.


## Uso

  Após a instalação dos pacotes, no cliente, bastará se conectar ao Router, enviar sua requisição e aguardar pela resposta:

```cs
using var client = new RequestSocket(">tcp://localhost:5559");// Faz um connect ao Router, que deve rodar na porta 5559

client.SendFrame(content);// Envia um conjunto de frames com o conteúdo especificado em content

var response = client.ReceiveFrameBytes();// Aqui será chamada uma função bloqueante, aguardando uma resposta do servidor
```

No Broker, deverá ser disponibilizada duas portas para conexão do cliente e servidor. E implementada uma lógica de retransmissão dos dados entre eles:

```cs
var frontend = new RouterSocket("@tcp://127.0.0.1:5559");// Bind do Router na porta 5559, aguardando por conexões do cliente
var backend = new DealerSocket("@tcp://127.0.0.1:5560");// Bind do Dealer na porta 5560, aguardando por conexões do servidor

// Atualiza o evento que será chamado internamente quando uma mensagem do cliente chegar no Router
frontend.ReceiveReady += (s, e) =>
{
    var msg = e.Socket.ReceiveMultipartMessage();// Recebe a mensagem vinda do cliente
    backend.SendMultipartMessage(msg); // Retransmite ela ao servidor
};

// Atualiza o evento que será chamado internamente quando uma mensagem do servidor chegar no Dealer
backend.ReceiveReady += (s, e) =>
{
    var msg = e.Socket.ReceiveMultipartMessage();// Recebe a mensagem vinda do servidor
    frontend.SendMultipartMessage(msg); // Retransmite ela ao cliente
};

//Cria uma instancia de NetMQPoller, que possibilita ouvir vários eventos. Para nosso caso o Router e o Dealer
using var poller = new NetMQPoller { backend, frontend };

poller.Run();// Cria thread ficará ocupada escutando os eventos no Router e Dealer, chamando o ReceiveReady correspondente (configurado anteriormente)
```

No servidor, você deverá se conectar ao socket Dealer, aguardar por uma requisição vinda dele, processar ela e mandar o retorno ao Dealer para ser retransmitido ao cliente.

```cs
using var server = new ResponseSocket();

// Se conecta ao Dealer presente no nosso Broker
server.Connect("tcp://127.0.0.1:5560");

while (!stoppingToken.IsCancellationRequested)
{
    // Chama uma função bloqueante que irá aguardar pelo próximo frame de dados recebido pelo Dealer
    var request = server.ReceiveFrameString();

    var response = GeneratePdf(request);// Código do servidor aqui

    server.SendFrame(response);// Retorna uma resposta ao Dealer que irá retransmitir ao servidor
```



