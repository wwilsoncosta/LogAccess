# Projeto para manutenção de usuários
# Configuração do projeto MVC para execução.

# 1 - Criando o banco de dados
# Dentro do diretório "Data" existe o arquivo "script.sql", dentro desse arquivo existe o script para criação das tabelas e Procedures
# Selecionar o Bando de dados e executar.
# Dentro da mesma pasta temos outro arquivo chamado "InsertInicial.sql", ele tem o insert padrão para o primeiro usuário.
# Apos a execução dos arquivos SQL verificar se os objetos estão criados.

# 2 - Configuração o sitema no IIS
# Com o servidor IIS instalado siga os seguintes passos:
# Crie uma pasta no sistema de arquivos para hospedar o sistema.
# Crie um site do IIS para liberar o acesso ao sistema aponte para a pasta onde os fontes foram clonados..
# libere permissões de leitura à identidade do pool de aplicativos na pasta local.
# No menu Iniciar, aponte para Ferramentas Administrativas e clique em Gerenciador dos Serviços de Informações da Internet (IIS).
# No Gerenciador do IIS, no painel Conexões , expanda o nó do servidor (por exemplo, DEV01).
# Clique com o botão direito do mouse no nó Sites e clique em Adicionar Site.
# Na caixa Nome do site, digite um nome para o site do IIS (por exemplo, LogAccess).
# Na caixa Caminho físico , digite (ou navegue até) o caminho para sua pasta local (por exemplo, C:\LogAccess)
# Na caida Porta digite o número da porta onde o sistema irá executar.
# No painel Ações , em Editar Site, clique em Ligações.
# Na caixa de diálogo Ligações do Site, clique em Adicionar.
# Na caixa de diálogo Adicionar Associação de Site , defina o endereço IP e a Porta para corresponderem à configuração do site existente.
# Na caixa Nome do host, digite o nome do servidor Web (por exemplo, LogAccess) e clique em OK.

# Execução dos projetos no Visual Studio.
# Selecione o diretório onde o projeto foi clonado e configure o arquivo WEB.Config/AppSettings.json apontando para o banco de dados que será utilizado.
# Depois dessa configuração execute e o sistema irá abrir a página principal.


# 3 - Configuração da Web API.
#  Na api foram criados 5 End points para manutenção de usuários:
# 	3.1 - api/Usuarios/api/getusuarios - Esse endpoint retorna a lista de todos usuários que não foram deletados.
#	exempo de retorno 
			#[
			#  {
			#    "usuarioId": 0,
			#    "nome": "string",
			#    "login": "string",
			#    "senha": "string",
			#    "admin": "string",
			#    "isAdmin": 0
			#  }
			#]
#	3.2 - api/Usuarios/api/getusuarios{id} - Endpoint espera o id de um usuário para selecionar e mostrar as informações dele.
#	exempo de retorno 
			#[
			#	{
			#		"usuarioId": 0,
			#		"nome": "string",
			#		"login": "string",
			#		"senha": "string",
			#		"admin": "string",
			#		"isAdmin": 0
			#	}
			#]
#	3.3 -	api/Usuarios/ - Responsável por inserir um novo usuário no banco de dados.
		# 3.3.1 - O objeto a seguir deve ser informado para inserir um usuário.
#	exempo de retorno 
			#[
			#	{
			#		"usuarioId": 0,
			#		"nome": "string",
			#		"login": "string",
			#		"senha": "string",
			#		"admin": "string",
			#		"isAdmin": 0
			#	}
			#]
#	3.4 -	api/Usuarios/ - Responsável por atualizar os dados de um usuário no banco de dados.
		# 3.4.1 - O objeto a seguir deve ser informado para atualizar um usuário.
#	exempo de objeto para atualização 
			#[
			#	{
			#		"usuarioId": 0,
			#		"nome": "string",
			#		"login": "string",
			#		"senha": "string",
			#		"admin": "string",
			#		"isAdmin": 0
			#	}
			#]
#	3.5 -	api/Usuarios/ - Responsável por excluir um usuário no banco de dados.
		# 3.5.1 - Parâmetro "id" deve ser informado o código do usuário que irá ser deletado.
		
# 	3.6 - api/logacessos - Responsável por exibir os acessos por hora
#	exempo de retorno
			#[
			#  {
			#    "hora": "string",
			#    "quantidade": 0
			#  }
			#]
#	3.7 - api/Employees - Executa a consulta de um endpoint externo a aplição e retorna todos os empregados que tem a idade superior a 30 anos.
# 	exemplo de retorno
			#[
			#  {
			#    "id": 0,
			#    "nome": "string",
			#    "idade": 0
			#  }
			#]