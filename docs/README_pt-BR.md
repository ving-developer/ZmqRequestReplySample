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
<img alt="ZeroMQ version" src="https://img.shields.io/nuget/v/zeromq?logo=zeromq&logoColor=%23f00&label=ZMQ&color=red&link=https%3A%2F%2Fzguide.zeromq.org%2Fdocs%2Fpreface%2F
">
<img alt="dotnet 6" src="https://img.shields.io/badge/-.NET%206.0-blueviolet
">
</div>

## O que é o ØMQ?

 __ZeroMQ__, ou __ØMQ__, ou até mesmo __ZMQ__, é uma ferramenta voltada para comunicação asíncrona entre servidores. Ele oferece várias abordagens para comunicação via __socket__ utilizando [unicast](https://zguide.zeromq.org/docs/chapter2/#Unicast-Transports) ou [multicast](https://zguide.zeromq.org/docs/chapter5/).

 Sua implementação consiste em um sistema de mensagens muito leve, especialmente projetado para cenários de alto rendimento/baixa latência. Além disso, __ØMQ__ é totalmente desacoplado podendo implementar qualquer protocolo de mensageria, ao contrário de outras tecnologias como __RabbitMQ__, que nativamente implementa o __AMQP__.

<div align="center">
  <img width="800" src=".\assets\amqp-example.png"/>
  <br>
  <br>
</div>

Porém, ao se utilizar o __ØMQ__, a tratativa de cenários avançados como roteamento, balanceamento de carga, enfileiramento de mensagens e persistência de mensagens em caso de falhas ou reinicializações devem ser configuradas no próprio projeto.

No final das contas, o __ØMQ__ é mais leve pois possui muito menos implementações nativas. Mas também oferece total controle sobre o fluxo da sua aplicação, sendo interessante para casos de extrema complexidade e também casos onde o mínimo latência fará muita diferença.
