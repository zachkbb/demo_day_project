function zachwebapp_01() {

    var textSearch = document.getElementById("text-search");

    var dealTable = document.getElementById("deal-table");




       //Functions

       function searchDeals() {

        var url = 'http://localhost:5120/SearchDeals?search=' + textSearch.value;

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
                        showDeals(response.deals);
                    } else {
                        alert("API Error: " + response.message);
                    }
                } else {
                    alert("Server Error: " + xhr.status + " " + xhr.statusText);
                }
            }
        };

        function showDeals(deals) {
            var dealTableText = '<table class="table table-striped table-sm"><thead><tr><th scope="col">Deal ID</th><th scope="col">RestaurantId</th><th scope="col">DOW ID</th><th scope="col">Deal Name</th><th scope="col">Deal Day</th><th scope="col">Start Date</th><th scope="col">End Date</th></tr></thead><tbody>';
    
            for (var i = 0; i < deals.length; i++) {
                var deal = deals[i];
    
                dealTableText = dealTableText + '<tr><th scope="row">' + deal.dealId + '</th><td>' + deal.restaurantId + '</td><td>' + deal.dayOfWeekId + '</td><td>' + deal.dealName + '</td><td>' + deal.dealDay + '</td><td>' + deal.StartDate + '</td><td>' + deal.EndDate + '</td></tr>'; 
            }
    
            dealTableText = dealTableText + '</tbody></table>';
    
            dealTable.innerHTML = dealTableText;
        }
    
        function searchClear() {
            textSearch.value = "";
            searchDeals();
        }

    };

    // onload
    searchDeals();

}
zachwebapp_01();