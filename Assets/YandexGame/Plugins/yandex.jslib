mergeInto(LibraryManager.library, {
  Hello: function () {
    window.alert("Hello, world!");
    return "hello world";
  },
  
  ShowPrice: function () {
    if (myGameInstance && myGameInstance.SendMessage) {
      if (gameShop.length > 0) {
        // Устанавливаем цену 100 для нулевого индекса
      
        myGameInstance.SendMessage('InApp', 'SetPriceCode', gameShop[0].priceCurrencyCode);
        console.log('Setting price code to: ' + gameShop[0].priceCurrencyCode);
      } else {
        console.error('gameShop is empty.');
      }
    } else {
      console.error('myGameInstance or SendMessage not available.');
    }
  },
});
