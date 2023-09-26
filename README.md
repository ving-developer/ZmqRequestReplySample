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
<img alt="ZeroMQ version" src="https://img.shields.io/nuget/v/zeromq?logo=zeromq&logoColor=%23f00&label=ZMQ&color=red&link=https%3A%2F%2Fzguide.zeromq.org%2Fdocs%2Fpreface%2F
">
<img alt="dotnet 6" src="https://img.shields.io/badge/-.NET%206.0-blueviolet
">
</div>

## What is ØMQ?

  __ZeroMQ__, or __ØMQ__, or even __ZMQ__, is a tool aimed at asynchronous communication between servers. It offers several approaches for communicating via __socket__ using [unicast](https://zguide.zeromq.org/docs/chapter2/#Unicast-Transports) or [multicast](https://zguide.zeromq.org/docs/chapter5 /).

  Its implementation consists of a very lightweight messaging system, specially designed for high throughput/low latency scenarios. Furthermore, __ØMQ__ is completely decoupled and can implement any messaging protocol, unlike other technologies such as __RabbitMQ__, which natively implements __AMQP__.

<div align="center">
   <img width="800" src=".\assets\amqp-example.png"/>
   <br>
   <br>
</div>

However, when using __ØMQ__, the handling of advanced scenarios such as routing, load balancing, message queuing and message persistence in case of failures or restarts must be configured in the project itself.

Ultimately, __ØMQ__ is lighter as it has far fewer native implementations. But it also offers total control over the flow of your application, making it interesting for cases of extreme complexity and also cases where minimal latency will make a big difference.