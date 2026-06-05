describe('produtos', () => {

  it('carregar produtos', () => {
    //arrange
    cy.visit('http://localhost:4200/shop');
    //act
    cy.get('[name="search"]').type('blue');
    cy.get('[data-test="btn-search-shop"]').click();
    //assert
    cy.get('[data-test="card-product"]').should('have.length', 3);
  });

  it('carregar produtos sem resultado', () => {
    //arrange
    cy.visit('http://localhost:4200/shop');
    //act
    cy.get('[name="search"]').type('blue123');
    cy.get('[data-test="btn-search-shop"]').click();
    //assert
    cy.get('[data-test="card-product"]').should('have.length', 0);
  });

});

