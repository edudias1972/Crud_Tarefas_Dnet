describe('AuthGuard', () => {
  it('deve bloquear acesso a rota admin sem login', () => {
    cy.visit('http://localhost:4200/admin');
    cy.url().should('include', '/login');
  });

  it('deve permitir acesso a rota admin com token válido', () => {
    // Simula login salvando token no localStorage
    const fakeToken = [
      'header',
      btoa(JSON.stringify({ role: 'Admin' })),
      'signature'
    ].join('.');

    cy.window().then(win => {
      win.localStorage.setItem('token', fakeToken);
    });

    cy.visit('http://localhost:4200/admin');
    cy.url().should('include', '/admin');
  });

  it('deve bloquear acesso a rota admin se role for User', () => {
    const fakeToken = [
      'header',
      btoa(JSON.stringify({ role: 'User' })),
      'signature'
    ].join('.');

    cy.window().then(win => {
      win.localStorage.setItem('token', fakeToken);
    });

    cy.visit('http://localhost:4200/admin');
    cy.url().should('include', '/acesso-negado');
  });
});
