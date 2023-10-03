<p align="center"></p>
<p align="center">
  <span>🡇&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
  <br>
  <strong><a href="#">English 🇬🇧</a></strong>
  ·
  <a href="/docs/README_pt-BR.md">Português  🇧🇷</a>
</p>

<br>
<br>

<div align="center">
  <img width="200" alt="zeromq logo" src=".\docs\assets\zeromq.png" href="https://zguide.zeromq.org/docs/preface/"/>
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

## What is ØMQ?

  __ZeroMQ__, or __ØMQ__, or even __ZMQ__, is a tool aimed at asynchronous communication between servers. It offers several approaches for communicating via __socket__ using [unicast](https://zguide.zeromq.org/docs/chapter2/#Unicast-Transports) or [multicast](https://zguide.zeromq.org/docs/chapter5/).

  Its implementation consists of a very lightweight messaging system, specially designed for high throughput/low latency scenarios. Furthermore, __ØMQ__ is completely decoupled and can implement any messaging protocol, unlike other technologies such as __RabbitMQ__, which natively implements __AMQP__.

<div align="center">
   <img width="800" src=".\docs\assets\amqp-example.png"/>
   <br>
   <br>
</div>

However, when using __ØMQ__, the handling of advanced scenarios such as routing, load balancing, message queuing and message persistence in case of failures or restarts must be configured in the project itself.

Ultimately, __ØMQ__ is lighter as it has far fewer native implementations. But it also offers total control over the flow of your application, making it interesting for cases of extreme complexity and also cases where minimal latency will make a big difference.

## Messaging patterns supported

In __ØMQ__, messages are divided into multiple parts, where each part is called a _frame_. This will be used to add "headers" to each _frame_. zmq itself will either deliver all the _frames_ or none of them.

No __ØMQ__, the existing connection types (__socket__) are:

- [inproc](http://api.zeromq.org/master:zmq-inproc) - using the `zmq_inproc()` function to transport messages via memory directly between threads that share a single __ØMQ__ context. It can be used in [__Exclusive-pair__](https://zguide.zeromq.org/docs/chapter2/#Messaging-Patterns) patterns, as shown in the documentation itself.
  
- [ipc](http://api.zeromq.org/master:zmq-ipc) - uses `zmq_ipc()` to transport messages between local processes using a system-dependent IPC mechanism. It can be used to implement a [__Pipeline__](https://zguide.zeromq.org/docs/chapter2/#Messaging-Patterns) service as shown in the documentation. Currently it only works on UNIX servers.
  
- [tcp](http://api.zeromq.org/master:zmq-tcp) - uses `zmq_tcp()` to establish an asynchronous remote connection between servers. It is widely used in [__Request-Reply__](https://zguide.zeromq.org/docs/chapter2/#Messaging-Patterns), where data traffic will not be done with __HTTP__. Instead, use the __ØMQ__ pattern itself with a specified length and message. The image below illustrates how data is passed through __socket__.

<div align="center">
   <img width="200" src=".\docs\assets\zmq-protocol.png"/>
   <br>
   <br>
</div>

- [pgm](http://api.zeromq.org/master:zmq-pgm) - uses `zmq_pgm()` to implement a multicast strategy, where data frames are sent via a set of IP addresses. It can be used in [__Pub-sub__](https://zguide.zeromq.org/docs/chapter2/#Messaging-Patterns) patterns as shown in the documentation.

- [epgm](http://api.zeromq.org/master:zmq-epgm) - can be activated through `zmq_epgm()`, it is similar to normal pgm, but here the data frames travel via UDP. It can be used in [__Pub-sub__](https://zguide.zeromq.org/docs/chapter2/#Messaging-Patterns) patterns as shown in the documentation.


## Request-Reply

It is a communication protocol where the client makes a request to the server, waits for a response while the server processes the request, and finally returns a response to the client. To implement this use case, [__ØMQ__](https://zguide.zeromq.org/) offers packaging that separates the message payload from an envelope.

The envelope will contain the return address for responses. This is how even __ØMQ__ does not store state, it can implement request-response applications. The image below illustrates a request received by the server, where _frame 1_ is the address of the client that made the request. You can capture this address and send a response to it.

<div align="center">
   <img width="600" src=".\docs\assets\zmq-frames.jpg"/>
   <br>
   <br>
</div>