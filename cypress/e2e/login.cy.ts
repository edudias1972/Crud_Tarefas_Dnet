describe('Login Flow', () => {
  beforeEach(() => {
    cy.visit('http://localhost:4200/login');
  });

  it('deve exibir mensagem de alerta se campos estiverem vazios', () => {
    cy.get('button[type="submit"]').click();
    cy.contains('Preencha todos os campos corretamente.').should('be.visible');
  });

  it('deve realizar login com sucesso e redirecionar para tarefas', () => {
    cy.get('#email').type('admin@teste.com');
    cy.get('#senha').type('123456');
    cy.get('button[type="submit"]').click();

    cy.contains('Login realizado com sucesso!').should('be.visible');
    cy.url().should('include', '/tarefas');
  });

  it('deve exibir mensagem de erro se credenciais forem inválidas', () => {
    cy.get('#email').type('user@teste.com');
    cy.get('#senha').type('senhaerrada');
    cy.get('button[type="submit"]').click();

    cy.contains('Credenciais inválidas. Tente novamente.').should('be.visible');
  });
});
