function zachwebapp_01() {


    //Get elements

    var textSearch = document.getElementById("text-search");

    var buttonSearch = document.getElementById("button-search");
    var buttonSearchClear = document.getElementById("button-search-clear");

    var dealTable = document.getElementById("deal-table");

    var buttonInsert = document.getElementById("button-insert");
    var buttonInsertCancel = document.getElementById("button-insert-cancel");

    var buttonUpdate = document.getElementById("button-update");
    var buttonUpdateCancel = document.getElementById("button-update-cancel");

    var buttonDelete = document.getElementById("button-delete");
    var buttonDeleteCancel = document.getElementById("button-delete-cancel");

    //Event listeners

    buttonSearch.addEventListener("click", searchDeals);
    buttonSearchClear.addEventListener("click", searchClear);

    buttonInsert.addEventListener("click", insertDeal);
    buttonInsertCancel.addEventListener("click", insertDealCancel);

    buttonUpdate.addEventListener("click", updateDeal);
    buttonUpdateCancel.addEventListener("click", updateDealCancel);

    buttonDelete.addEventListener("click", deleteDeal);
    buttonDeleteCancel.addEventListener("click", deleteDealCancel);


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

            var url = 'http://localhost:5120/InsertDeal?RestaurantId=' + textRestaurantId.value + '&DayOfWeekId=' + textDayOfWeekId.value + '&DealName=' + textDealName.value + '&DealDay=' + textDealDay.value + '&StartDate=' + textStartDate.value + '&EndDate=' + textEndDate.value;
    
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

        function updateDeal() {

            var textDealId = document.getElementById("text-update-deal-id");
            var textRestaurantId = document.getElementById("text-update-restaurant-id");
            var textDayOfWeekId = document.getElementById("text-update-day-of-week-id");
            var textDealName = document.getElementById("text-update-deal-name");
            var textDealDay = document.getElementById("text-update-deal-day");
            var textStartDate = document.getElementById("text-update-start-date");
            var textEndDate = document.getElementById("text-update-end-date");
    
            var url = 'http://localhost:5120/UpdateDeals?DealId=' + textDealId.value + '&RestaurantId=' + textRestaurantId.value + '&DayOfWeekId=' + textDayOfWeekId.value + '&DealName=' + textDealName.value + '&DealDay=' + textDealDay.value + '&StartDate=' + textStartDate.value + '&EndDate=' + textEndDate.value;
    
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = doAfterUpdateDeal;
            xhr.open('GET', url);
            xhr.send(null);
    
            function doAfterUpdateDeal() {
                var DONE = 4; // readyState 4 means the request is done.
                var OK = 200; // status 200 is a successful return.
                if (xhr.readyState === DONE) {
                    if (xhr.status === OK) {
    
                        var response = JSON.parse(xhr.responseText);
    
                        if (response.result === "success") {
                            showDeals(response.employees);
                        } else {
                            alert("API Error: " + response.message);
                        }
                    } else {
                        alert("Server Error: " + xhr.status + " " + xhr.statusText);
                    }
                }
            };
    
            textDealId.value = "";
            textRestaurantId.value = "";
            textDayOfWeekId.value = "";
            textDealName.value = "";
            textDealDay.value = "";
            textStartDate.value = "";
            textEndDate.value = "";
    
        };

        function updateDealCancel() {

            var textDealId = document.getElementById("text-update-deal-id");
            var textRestaurantId = document.getElementById("text-update-restaurant-id");
            var textDayOfWeekId = document.getElementById("text-update-day-of-week-id");
            var textDealName = document.getElementById("text-update-deal-name");
            var textDealDay = document.getElementById("text-update-deal-day");
            var textStartDate = document.getElementById("text-update-start-date");
            var textEndDate = document.getElementById("text-update-end-date");
    
            textDealId.value = "";
            textRestaurantId.value = "";
            textDayOfWeekId.value = "";
            textDealName.value = "";
            textDealDay.value = "";
            textStartDate.value = "";
            textEndDate.value = "";
    
        };

        function deleteDeal() {

            var textDealId = document.getElementById("text-delete-deal-id");
    
            var url = 'http://localhost:5120/DeleteDeals?DealId=' + textDealId.value;
    
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = doAfterDeleteDeal;
            xhr.open('GET', url);
            xhr.send(null);
    
            function doAfterDeleteDeal() {
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
    
            textDealId.value = "";
    
        };

        function deleteDealCancel() {
            var textDealId = document.getElementById("text-delete-deal-id");
            textDealId.value = "";
        };
    

    

    // onload
    searchDeals();

}
zachwebapp_01();