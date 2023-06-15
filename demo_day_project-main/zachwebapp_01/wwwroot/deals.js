function zachwebapp_01() {


    //Get elements

    var textSearch = document.getElementById("text-search");

    var buttonSearch = document.getElementById("button-search");
    var buttonSearchClear = document.getElementById("button-search-clear");

    var dealTable = document.getElementById("deal-table");

    var buttonInsert = document.getElementById("button-insert");
    var buttonInsertCancel = document.getElementById("button-insert-cancel");

    //Event listeners

    buttonSearch.addEventListener("click", searchDeals);
    buttonSearchClear.addEventListener("click", searchClear());

    buttonInsert.addEventListener("click", insertDeal());
    buttonInsertCancel.addEventListener("click", insertDealCancel());


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
        };
    
        function searchClear() {
            textSearch.value = "";
            searchDeals();
        };

        function insertDeal() {

            var textRestaurantId = document.getElementById("text-insert-restaurant-id");
            var textDayOfWeekId = document.getElementById("text-insert-day-of-week-id");
            var textDealName = document.getElementById("text-insert-deal-name");
            var textDealDay = document.getElementById("text-insert-deal-day");
            var textStartDate = document.getElementById("text-insert-start-date");
            var textEndDate = document.getElementById("text-insert-end-date");

            var url = 'http://localhost:5120/InsertDeal?RestaurantId=' + textRestaurantId.value + '&DayOfWeekId=' + textDayOfWeekId.value + '&DealName=' + textDealName.value + '&DealDay=' + textDealDay.value + '&StartDate=' + textStartDate.value + '&EndDate' + textEndDate.value;
    
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = doAfterInsertDeal;
            xhr.open('GET', url);
            xhr.send(null);
    
            function doAfterInsertDeal() {
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
    
            textRestaurantId.value = "";
            textDayOfWeekId.value = "";
            textDealName.value = "";
            textDealDay.value = "";
            textStartDate.value = "";
            textEndDate.value = "";
    
        };

        function insertDealCancel() {

            var textRestaurantId = document.getElementById("text-insert-restaurant-id");
            var textDayOfWeekId = document.getElementById("text-insert-day-of-week-id");
            var textDealName = document.getElementById("text-insert-deal-name");
            var textDealDay = document.getElementById("text-insert-deal-day");
            var textStartDate = document.getElementById("text-insert-start-date");
            var textEndDate = document.getElementById("text-insert-end-date");
    
            textRestaurantId.value = "";
            textDayOfWeekId.value = "";
            textDealName.value = "";
            textDealDay.value = "";
            textStartDate.value = "";
            textEndDate.value = "";
    
        };

    };

    // onload
    searchDeals();

}
zachwebapp_01();