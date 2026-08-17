# Exemplo de Integração Externa com Secullum RH
Esta é uma aplicação de exemplo para integração de dados via *webservice* com o [Secullum RH](https://pontoweb.secullum.com.br/).
Foi desenvolvida em *C# Winforms* usando *Framework 4.5*.
Para utilizar a integração externa, é necessário que as requisições sejam feitas com o protocolo TLS(Transport Layer Security) na versão 1.2 ou superior.

# Interface baseada em OpenApi
A documentação com [Swagger](https://pontowebintegracaoexterna.secullum.com.br/docs/) está disponível para execução e validação das requisições.

#### **Importante:**
* Para realizar a integração, será necessário uma **Conta Secullum** com pelo menos um banco de dados do **Secullum RH** ativo utilizando o plano PRO ou superior. 
* Além disso é necessário habilitar a integração externa dentro do **Secullum RH**, via menu Manutenção > Integração com Sistemas.
* Leia o [Manual da Integração](https://github.com/Secullum/PontoWebIntegracaoExternaExemplo/blob/master/Integracao_Externa.pdf) atentamente para entender o fluxo de dados.
  
Em caso de dúvidas, consulte o suporte técnico.
