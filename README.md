# Teste Técnico .NetCore WebCrawler
## :memo: Descrição do projeto


### Requisitos
- Acessar o site ```https://proxyservers.pro/proxy/list/order/updated/order```✔️
- Extrair os campos "IP Adress", "Port", "Country" e "Protocol" de todas as linhas, de todas as páginas disponíveis na execução.✔️
- Necessário salvar o resultado da extração em arquivo json, que deverá ser salvo na máquina.✔️
- Necessário salvar em banco de dados a data início da execução, data término da execução, quantidade de páginas analisadas, quantidade de linhas extraídas em todas as páginas e arquivo json gerado.✔️
- Necessário armazenar o arquivo .html de cada página.✔️
- Necessário que o webcrawler seja multiThread, com máximo de 3 execuções simultâneas.✔️

### Solução
Desenvolvi o web crawler utilizando .NET Core, MediatR e um banco de dados NoSQL MongoDB para persistência dos dados. A aplicação foi projetada com eventos assíncronos, visando a atualização do JSON com os dados extraídos e a persistência desses dados no banco. Além disso, incluí um endpoint GET para consulta dos dados inseridos.

O web crawler realiza as seguintes atividades:

- Extração de Dados: Acessa o site específico para coletar informações como "IP Address", "Port", "Country" e "Protocol" de todas as linhas disponíveis em todas as páginas.

- Armazenamento em JSON: Os dados extraídos são atualizados em um arquivo JSON, facilitando o armazenamento e uso posterior dessas informações.

- Persistência no MongoDB: Os dados coletados são armazenados de forma persistente no banco de dados NoSQL MongoDB, garantindo uma estrutura organizada e de fácil acesso.

- Funcionamento Assíncrono: A aplicação opera com eventos assíncronos para melhor eficiência e desempenho, permitindo que as tarefas de extração, atualização e persistência dos dados ocorram de forma paralela.

- Endpoint GET para Consulta de Dados: Implementei um endpoint GET que possibilita consultar os dados previamente inseridos no banco de dados, permitindo acesso rápido e prático às informações coletadas.

A integração de tecnologias como MediatR para gerenciamento de eventos e MongoDB para armazenamento eficiente dos dados possibilita uma solução robusta e escalável para o processo de web crawling.

## :wrench: Tecnologias Utilizadas

C#, .NetCore, MediatR, MongoDB e Selenium
- Tecnologias Utilizadas:
  <div>
  <img align="center" height="50em" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg" />
  <img align="center" alt="" height="50" width="65" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg" />
  <img align="center" alt="" height="40" width="50" src="https://github.com/DevPhde/DevPhde/assets/113299561/74cccb29-0752-4945-ac78-3833e8f3731f" />
  <img align="center" alt="" height="40" width="50" src="https://github.com/DevPhde/iPrazos_TesteTecnico/assets/113299561/c01cd40d-aeb9-407c-8235-d022cd0828ea"/>
  <img align="center" alt="MONGODB" height="60" width="70" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/mongodb/mongodb-original-wordmark.svg" /> 
  </div>
  
## :dart: Status do projeto
Teste concluído.
