function zachwebapp_01() {

    //Get elements
    
    var textSearch = document.getElementById("text-search");
    var dealTable = document.getElementById("deal-table");

    var buttonSearch = document.getElementById("button-search");
    var buttonSearchClear = document.getElementById("button-search-clear");

    //Event listeners

    buttonSearch.addEventListener("click", searchDeals);
    buttonSearchClear.addEventListener("click", searchClear);

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

        function showDeals(deals) {
            var dealTableText = '<table style="text-align: left;" class="table table-striped table-sm"><thead><tr><th scope="col">Restaurant Name</th><th scope="col">Deal Name</th><th scope="col">Deal Day</th><th scope="col">Start Date</th><th scope="col">End Date</th></tr></thead><tbody>';
    
            for (var i = 0; i < deals.length; i++) {
                var deal = deals[i];
    
                dealTableText = dealTableText + '</th><td>' + deal.restaurantName + '</td><td>' + deal.dealName + '</td><td>' + deal.dealDay + '</td><td>' + deal.startDate.split(' ')[0] + '</td><td>' + deal.endDate.split(' ')[0] + '</td></tr>'; 
            }
    
            dealTableText = dealTableText + '</tbody></table>';
    
            dealTable.innerHTML = dealTableText;
        };

       };

        function searchClear() {
            textSearch.value = "";
            searchDeals();
        };

        // onload
    searchDeals();

}

zachwebapp_01();