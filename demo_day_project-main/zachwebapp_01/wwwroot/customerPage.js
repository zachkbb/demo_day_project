function zachwebapp_01() {

    //Get elements
    
    var textSearch = document.getElementById("text-search");
    var dealTable = document.getElementById("deal-table");

    var buttonSearch = document.getElementById("button-search");
    var buttonSearchClear = document.getElementById("button-search-clear");

    var buttonShowTodaysDeals = document.getElementById("button-show-todays-deals");
    
    var buttonShowAllDeals = document.getElementById("button-show-all-deals");


    //Event listeners

    buttonSearch.addEventListener("click", searchDeals);
    buttonSearchClear.addEventListener("click", searchClear);

    buttonShowTodaysDeals.addEventListener("click", searchTodaysDeals);

    buttonShowAllDeals.addEventListener("click", searchAllDeals);


    function searchDeals() {

        var url = 'http://localhost:5120/SearchCustomerDeals?search=' + textSearch.value;

        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = doAfterSearchDeals;
        xhr.open('GET', url);
        xhr.send(null);

        function doAfterSearchDeals() {
            var DONE = 4; // readyState 4 means the request is done.
            var OK = 200; // status 200 is a successful return.
            if (xhr.readyState === DONE) {
                if (xhr.status === OK) {

                    var response = JSON.parse(xhr.responseText);

                    if (response.result === "success") {
                        showDeals(response.customerPage);
                    } else {
                        alert("API Error: " + response.message);
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        };

    };

    function showDeals(deals) {
        var dealTableText = '<table id="table-striped;" style="text-align: left; border: 1px solid black !important;" class="table table-striped"><thead><tr><th scope="col">Restaurant Name</th><th scope="col">Deal Name</th><th scope="col">Deal Day</th><th scope="col">Start Date</th><th scope="col">End Date</th><th scope="col">Website</th></tr></thead><tbody>';
      
        for (var i = 0; i < deals.length; i++) {
          var deal = deals[i];
      
          dealTableText = dealTableText + '</th><td>' + deal.restaurantName + '</td><td>' + deal.dealName + '</td><td>' + deal.dealDay + '</td><td>' + deal.startDate.split(' ')[0] + '</td><td>' + deal.endDate.split(' ')[0] + '</td><td><a target=\"_blank\" href=\"' + deal.website + '\">Go to website</a></td></tr>';
        }
      
        dealTableText = dealTableText + '</tbody></table>';
      
        dealTable.innerHTML = dealTableText;
      }
      

        function searchClear() {
            textSearch.value = "";
            searchDeals();
        };

        function searchTodaysDeals() {
            var url = 'http://localhost:5120/SearchTodaysDeals?search=' + textSearch.value;

            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = doAfterSearchDeals;
            xhr.open('GET', url);
            xhr.send(null);
    
            function doAfterSearchDeals() {
                var DONE = 4; // readyState 4 means the request is done.
                var OK = 200; // status 200 is a successful return.
                if (xhr.readyState === DONE) {
                    if (xhr.status === OK) {
    
                        var response = JSON.parse(xhr.responseText);
    
                        if (response.result === "success") {
                            showDeals(response.customerPage);
                        } else {
                            alert("API Error: " + response.message);
                        }
                    } else {
                        alert("Server Error: " + xhr.status + " " + xhr.statusText);
                    }
                }
            };
        }

        function searchAllDeals() {
            searchDeals();
        }

        // onload
    searchDeals();

}

zachwebapp_01();