var detailsPage = $("body.page-detailsPage").length > 0;
var homePage = $("body.page-homePage").length > 0;


homePage && $(document).ready(function () {
    $('form').submit(function (e) {
        e.preventDefault();

        var companyId = $('#companyId').val();
        var subsidiaryId = $('#subsidiaryId').val();
        var postData = {
            companyId: companyId,
            subsidiaryId: subsidiaryId
        };

        $.ajax({
            url: 'http://localhost:5125/api/Document/GetDocumentsByCompanyAndSubsId',
            type: 'POST',
            data: JSON.stringify(postData),
            contentType: 'application/json',
            success: function (response) {
                var data = response.data;
                console.log(data);
                for (let i = 0; i < data.length; i++) {
                    let randomColor = '#' + (Math.random() * 0xFFFFFF << 0).toString(16);
                    let formattedDate = new Date(data[i].createDate).toLocaleDateString();

                    $('#tsmCardContainer').append(`
                        <div id="tsmReturnDetail" role="button" class="col-md-6 col-xl-4" data-id="${data[i].documentId}">
                            <div class="card">
                                <div class="card-body" style="border-bottom: 3px solid ${randomColor};">
                                    <div class="mb-4">
                                        <span class="badge badge-soft-primary float-end">${formattedDate}</span>
                                        <h5 class="card-title mb-0 text-truncate text-secondary">${data[i].accountOwnerInformation}</h5>
                                    </div>
                                    <div class="mb-4">
                                        <h5 class="card-title mb-0 fs-6">Document Reference Number:  <b>${data[i].transactionReferenceNumber}</b></h5>
                                    </div>
                                    <div class="mb-4">
                                        <h5 class="card-title mb-0 fs-6">Account Owner Code:  <b>${data[i].accountOwnerCode}</b></h5>
                                    </div>
                                    <div class="mb-4">
                                        <h5 class="card-title mb-0 d-flex fs-6">Closing Available Balance:
                                            <span class="d-flex">
                                                <b>$${data[i].closingAvailableBalance.balance}</b>
                                            </span>
                                        </h5>                                
                                    </div>
                                </div>
                            </div>
                        </div>
                    `);
                }

            },
        })

    });
    $(document).on('click', '#tsmReturnDetail', function () {
        var documentId = $(this).data('id');
        window.location.href = `./details.html?documentId=${documentId}`;
    });
});

detailsPage && $(document).ready(function () {
    const urlParams = new URLSearchParams(window.location.search);
    const documentId = urlParams.get('documentId');

    $.ajax({
        url: 'http://localhost:5125/api/Document/GetDocumentDetails?documentId=' + documentId,
        type: 'GET',
        contentType: 'application/json',
        success: function (response) {
            if (response.isSuccess) {
                var data = response.data;
                var transactions = data.transactions;
                var openingBalance = data.openingBalance;
                var closingBalance = data.closingBalance;
                var forwardAvailableBalance = data.forwardAvailableBalance;
                var closingAvailableBalance = data.closingAvailableBalance;

                $('#accountOwnerInformation').text(data.accountOwnerInformation);
                $('#accountOwnerCode').text(data.accountOwnerCode);
                $('#identifierCode').text(data.identifierCode);
                $('#relatedReference').text(data.relatedReference != null ? data.relatedReference : "There isn't any related file");
                $('#sequenceNumber').text(data.sequenceNumber);

                transactions.forEach((data, index) => {
                    console.log(data);
                    //let formattedDate = transaction.transactionDate.replace(/(\d{2})(\d{2})(\d{2})/, '$1/$2/$3');
                    $('.transactionItemList').append(`
                        <tr>
                            <td>
                                ${data.processDescription.toLowerCase().replace(/\b\w/g, c => c.toUpperCase())}
                            </td>
                            <td>${data.amount}</td>
                            <td>
                                <button type="button" id="openTransactionPopup" class="btn btn-warning waves-effect waves-light w-100" data-bs-toggle="modal" data-bs-target="#centermodal" data-transaction-id="${index}">                                                
                                    Show Details
                                </button>
                            </td>
                        </tr>
                    `);
                });

                $('.openingBalanceItemList').find('span').each(function() {
                    var className = $(this).attr('class').split(' ')[0];
                    $(this).find('b').text(openingBalance[className]);
                });
                $('.closingBalanceItemList').find('span').each(function() {
                    var className = $(this).attr('class').split(' ')[0];
                    $(this).find('b').text(closingBalance[className]);
                });
                $('.forwardAvailableBalance').find('span').each(function() {
                    var className = $(this).attr('class').split(' ')[0];
                    $(this).find('b').text(forwardAvailableBalance[className]);
                });
                $('.closingAvailableBalance').find('span').each(function() {
                    var className = $(this).attr('class').split(' ')[0];
                    $(this).find('b').text(closingAvailableBalance[className]);
                });
            }

        },
    });

    $(document).on('click', '#openTransactionPopup', function () {
        var transactionId = $(this).data('transaction-id');
        $.ajax({
            url: 'http://localhost:5125/api/Document/GetDocumentDetails?documentId=' + documentId,
            type: 'GET',
            contentType: 'application/json',
            success: function (response) {
                if (response.isSuccess) {
                    var data = response.data;
                    var transactions = data.transactions[transactionId];
                    $("#centermodal").find('.modal-body').html(`
                        <div class="d-grid gap-2 closingAvailableBalance">
                            <span class="amount d-flex justify-content-between">Amount <b>${transactions.amount}</b></span>
                            <span class="bookedDate d-flex justify-content-between">Booked Date <b>${transactions.bookedDate}</b></span>
                            <span class="currencyCodeLastLetter d-flex justify-content-between">Currency Code LastLetter <b>${transactions.currencyCodeLastLetter}</b></span>
                            <span class="documentNumber d-flex justify-content-between">Document Number <b>${transactions.documentNumber}</b></span>
                            <span class="hasAcocuntOwnerData d-flex justify-content-between">Has Acocunt OwnerData <b>${transactions.hasAcocuntOwnerData}</b></span>
                            <span class="identificationCodeEnum d-flex justify-content-between">Identification Code Enum <b>${transactions.identificationCodeEnum}</b></span>
                            <span class="processDescription d-flex justify-content-between">Process Description <b>${transactions.processDescription}</b></span>
                            <span class="transactionDate d-flex justify-content-between">Transaction Date <b>${transactions.transactionDate}</b></span>
                            <span class="transactionProcessType d-flex justify-content-between">Transaction ProcessType <b>${transactions.transactionProcessType}</b></span>
                            <span class="transactionType d-flex justify-content-between">Transaction Type <b>${transactions.transactionType}</b></span>
                        </div>
                        <span></span>
                    
                    `);
                }
            }
        });
        
    });

});