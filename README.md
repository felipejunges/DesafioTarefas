# Desafio Tarefas

## Autor

- [@felipejunges](https://www.github.com/felipejunges)

## Como rodar o projeto

Clone o projeto

```bash
git clone https://github.com/felipejunges/DesafioTarefas.git
```

Entre no diretório raiz do mesmo

```bash
cd DesafioTarefas
```

Suba o Docker Compose

```bash
docker compose up
```

Serão criados dois contâiners:
- DesafioTarefas.Api
- SqlServer 2019

### Para acessar o projeto:

Com os contâiners rodando, acesse:

http://localhost:5000/swagger/index.html

## Refinamento

- O cliente não avalia ter Status personalizados? Inicialmente, poderiam ser carregados os padrão, mas poderíamos implementar um cadastro de Status;
- Consulta de tarefas atrasadas não seria interessante?
- Não seria ideal soft delete das tarefas e projetos, por segurança?
- Não seria ideal uma trava para que usuários não admin não possam manipular projetos e tarefas de outros usuários?

## Melhorias técnicas

- TarefasRepository: melhorar a forma de persistir o Historicos e Comentarios. Foi feita desta forma para a gestão dos itens ser feita no Tarefa entity, porém, ao adicionar um item na lista, o objeto adicionado fica com seu state "Detached", de forma que não é incluído;
- Projeto CrossCutting: para implementar todos os interesses de todas as camadas da solução, principalmente, a injeção de dependência;
- Implementar a segurança na camada API, recebendo por exemplo, um token JWT, inclusive com controle de Role ou Policy (rota admin, por exemplo). Não foi implementado pois não seria possível gerar um token já que a login não faz parte do escopo do projeto;
- Avaliar uma estruturação melhor, ao invés de namespaces por 'tipo' de classe, por módulo;
- Log com [seq](https://docs.datalust.co/docs/getting-started-with-docker).