$(document).ready(function () {
    $('.form-image').click(function () { $('#customFile').trigger('click'); });
    $(function () {
        $('.selectpicker').selectpicker();
    });
    setTimeout(function () {
        $('body').addClass('loaded');
    }, 200);

    jQueryModalGet = (url, title) => {
        try {
            $.ajax({
                type: 'GET',
                url: url,
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#form-modal .modal-body').html(res.html);
                    $('#form-modal .modal-title').html(title);
                    $('#form-modal').modal('show');
                    console.log(res);
                },
                error: function (err) {
                    console.log(err)
                }
            })
            //to prevent default form submit event
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }

    jQueryModalPost = form => {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isValid) {
                        $('#viewAll').html(res.html)
                        $('#form-modal').modal('hide');
                    }
                },
                error: function (err) {
                    console.log(err)
                }
            })
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }
    jQueryModalDelete = (url) => {
        if (confirm('Are you sure to delete this record ?')) {
            try {
                $.ajax({
                    type: 'DELETE',
                    url: url,
                    //data: new FormData(form),
                    contentType: false,
                    processData: false,
                    success: function (res) {
                        if (res.isValid) {
                            $('#viewAll').html(res.html)
                        }
                    },
                    error: function (err) {
                        console.log(err)
                    }
                })
            } catch (ex) {
                console.log(ex)
            }
        }

        //prevent default form submit event
        return false;
    }





    /// Charts
    var xValues = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"];
    var yValues = [500, 100, 1500, 2000, 2500, 1000, 3500];
    var barColors = ["#17a2b8", "#28a745", "#ffc107", "#dc3545", "#643eec", "#17a2b8", "#28a745", "#ffc107"];

    new Chart("myChart", {
        type: "line",
        data: {
            labels: xValues,
            datasets: [{
                backgroundColor: barColors,
                data: yValues,
                borderRadius: "200"
            }]
        },
        options: {
            legend: { display: false },
            title: {
                display: true,
                text: "Lorem Ipsum"
            }
        }
    });

    /*new Chart("finChart", {
        type: 'line',
        data: {
            labels: ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"],
            datasets: [{
                label: 'Desposits',
                data: [65, 59, 80, 81, 56, 55, 40],
                borderColor: 'rgb(255, 99, 132)',
                yAxisID: 'y-axis-1',
            }, {
                label: 'Withdrawal',
                data: [28, 48, 35, 19, 40, 27, 35],
                borderColor: 'rgb(54, 162, 235)',
                yAxisID: 'y-axis-2',
            }]
        },
        options: {
            legend: {
                position: 'bottom',
                labels: {
                    fontColor: 'blue',
                    fontSize: 14,
                    boxWidth: 20,
                }
            },
            scales: {
                yAxes: [{
                    type: 'linear',
                    display: true,
                    position: 'left',
                    id: 'y-axis-1',
                }, {
                    type: 'linear',
                    display: true,
                    position: 'right',
                    id: 'y-axis-2',
                    gridLines: {
                        drawOnChartArea: false, // only want the grid lines for one axis to show up
                    },
                }],
            }
        }
    });*/

    // Fetch historical Bitcoin data
    function updateChart(url) {
        fetch(url)
            .then(response => response.json())
            .then(data => {
                const prices = data.prices;
                const chartLabels = prices.map(item => new Date(item[0]).toLocaleDateString());
                const chartData = prices.map(item => item[1]);

                const ctx = document.getElementById('bitcoinChart').getContext('2d');
                var gradient = ctx.createLinearGradient(0, 0, 0, 450);
                gradient.addColorStop(0, 'rgba(20, 25, 67, 0)');
                gradient.addColorStop(0.5, 'rgba(105, 32, 143, 0.5)');
                gradient.addColorStop(1, 'rgba(27, 35, 179, 1)');


                if (window.bitcoinChart instanceof Chart) {
                    window.bitcoinChart.destroy();
                }
                window.bitcoinChart = new Chart(ctx, {
                    type: 'line',
                    data: {
                        labels: chartLabels,
                        datasets: [{
                            label: 'Bitcoin Price (USD)',
                            data: chartData,
                            borderColor: '#8028ae',
                            tension: 0.5,
                            pointBackgroundColor: 'purple',
                            pointBorderColor: '#fff',
                            pointBorderWidth: 2,
                            pointRadius: 5,
                            pointHoverBackgroundColor: 'Blue',
                            pointHoverBorderColor: '#fff',
                            pointHoverBorderWidth: 3,
                            pointHoverRadius: 6,
                            fill: true,
                            backgroundColor: gradient,
                            borderWidth: 2
                        }]
                    },
                    options: {
                        plugins: {
                            tooltip: {
                                backgroundColor: 'linear-gradient(109.6deg, rgb(20 25 67) 11.2%, rgb(105 32 143) 53.7%, rgb(27 35 179) 100.2%)',
                                titleFontColor: '#ffffff',
                                bodyFontColor: '#ffffff',
                                borderColor: '#ffffff',
                                borderWidth: 1
                            }
                        },
                        animations: {
                            tension: {
                                duration: 1500,
                                easing: 'linear',
                                from: 1,
                                to: 0,
                                loop: true
                            }
                        },
                        scales: {
                            y: {
                                beginAtZero: false
                            }
                        }
                    }
                });
            });
    }

    // Initial chart load
    updateChart('https://api.coingecko.com/api/v3/coins/bitcoin/market_chart?vs_currency=usd&days=30&interval=daily');

    // Event listener for button click

    /*document.getElementById('interval1').addEventListener('click', function () {
        this.classList.add('btn-primary');
        $(this).siblings().removeClass("btn-primary");
        // Here you can modify the URL as needed
        const newURL = 'https://api.coingecko.com/api/v3/coins/bitcoin/market_chart?vs_currency=usd&days=1&interval=daily'; // Example URL
        updateChart(newURL);
    });*/
    document.getElementById('interval7').addEventListener('click', function () {
        this.classList.add('btn-primary');
        $(this).siblings().removeClass("btn-primary");

        // Here you can modify the URL as needed
        const newURL = 'https://api.coingecko.com/api/v3/coins/bitcoin/market_chart?vs_currency=usd&days=7&interval=daily'; // Example URL
        updateChart(newURL);
    });
    document.getElementById('interval15').addEventListener('click', function () {
        this.classList.add('btn-primary');
        $(this).siblings().removeClass("btn-primary");
        // Here you can modify the URL as needed
        const newURL = 'https://api.coingecko.com/api/v3/coins/bitcoin/market_chart?vs_currency=usd&days=15&interval=daily'; // Example URL
        updateChart(newURL);
    });
    document.getElementById('interval30').addEventListener('click', function () {
        this.classList.add('btn-primary');
        $(this).siblings().removeClass("btn-primary");
        // Here you can modify the URL as needed
        const newURL = 'https://api.coingecko.com/api/v3/coins/bitcoin/market_chart?vs_currency=usd&days=30&interval=daily'; // Example URL
        updateChart(newURL);
    });

    // Network tree
    //const mlmData = {
    //    name: "Root User",
    //    children: [
    //        {
    //            name: "User 1",
    //            children: [
    //                { name: "User 1.1", children: [] },
    //                { name: "User 1.2", children: [] },
    //            ],
    //        },
    //        {
    //            name: "User 2",
    //            children: [
    //                { name: "User 2.1", children: [] },
    //                { name: "User 2.2", children: [] },
    //                { name: "User 2.3", children: [] },
    //            ],
    //        },
    //        // Add more users as needed
    //    ],
    //};

    //const treeLayout = d3.tree().size([800, 500]);

    //const svg = d3
    //    .select("#mlm-tree")
    //    .append("svg")
    //    .attr("width", 900)
    //    .attr("height", 600)
    //    .append("g")
    //    .attr("transform", "translate(50, 50)");

    //const root = d3.hierarchy(mlmData);
    //const mlmTreeData = treeLayout(root);

    //// Draw links
    //svg
    //    .selectAll(".link")
    //    .data(mlmTreeData.links())
    //    .enter()
    //    .append("path")
    //    .attr("class", "link")
    //    .attr("d", d3.linkHorizontal().x((d) => d.y).y((d) => d.x));

    //// Draw nodes
    //const nodes = svg
    //    .selectAll(".node")
    //    .data(mlmTreeData.descendants())
    //    .enter()
    //    .append("g")
    //    .attr("class", "node")
    //    .attr("transform", (d) => `translate(${d.y},${d.x})`);

    //nodes
    //    .append("circle")
    //    .attr("r", 5)
    //    .style("fill", (d) => (d.depth === 0 ? "#f00" : "#00f"));

    //// Add labels to nodes
    //nodes
    //    .append("text")
    //    .attr("x", 8)
    //    .attr("y", 3)
    //    .attr("dy", ".35em")
    //    .attr("text-anchor", "start")
    //    .text((d) => d.data.name);



    // Tooltip
    $(function () {
        $('[data-toggle="tooltip"]').tooltip();
    });



    // Initialize a variable to store the last fetched BTC price
    var lastBtcPrice = null;

    setInterval(function () {
        // URL of the cryptocurrency exchange API
        var apiUrl = 'https://api.binance.com/api/v3/ticker/price?symbol=BTCUSDT';

        // Make AJAX request to fetch BTC price
        $.ajax({
            url: apiUrl,
            type: 'GET',
            success: function (response) {
                // Parse the response JSON
                var btcPrice = parseFloat(response.price).toFixed(2); // Assuming the API returns the price as a string

                // Determine the class based on price movement
                var priceClass = 'price-up'; // Default to 'price-up'
                if (lastBtcPrice !== null) { // Ensure there is a last price to compare
                    if (btcPrice < lastBtcPrice) {
                        priceClass = 'price-down';
                    }
                }

                // Update the HTML with the new price and appropriate class
                $('#btcPrice').html('Current BTC Price: $<span class="' + priceClass + '">' + btcPrice + '</span>');

                // Update lastBtcPrice for the next comparison
                lastBtcPrice = btcPrice;
            },
            error: function (xhr, status, error) {
                console.error('Error fetching BTC price:', error);
                $('#btcPrice').text('Error fetching BTC price');
            }
        });
    }, 1000);


});