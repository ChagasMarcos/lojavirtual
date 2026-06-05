describe('seleciona produto', () => {

  it('selecionarProduto', () => {
    //arrange
    cy.visit('http://localhost:4200/shop');
    //act
    cy.get('[data-test="card-product-Angular Blue Boots"]').click();
    //assert
    cy.url().should('include', 'http://localhost:4200/shop/18');
    cy.get('[data-test="product-detail-name"]').should('contain', 'Angular Blue Boots');
  });



});

