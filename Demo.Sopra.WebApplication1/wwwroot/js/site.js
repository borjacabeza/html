let App = {
    Core: {
        CreateTable: function(id) {
            $("#" + id).DataTable({
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
                }
            });           
        },
        GetOrderDetails: function(orderId) {
            // Método .post() de jQuery
            $.post("/pedidos/detalles/" + orderId, {}, function(data) {
                $(".modal-title").html("Detalle del Pedido " + orderId);
                $("#modalbody").html(data);
                $("#b1").html("Cerrar");
                $("#b2").hide();
                
                $("#modal").modal("show");           
            });
        },
        GetOrderDetails2: function(orderId) {
            // Método .ajax() de jQuery
            $.ajax({
                type: "POST",
                async: true,
                url: "/pedidos/detalles/" + orderId,
                data: {},
                headers: {},
                contentType: "application/json",
                accepts: "text/html",
                success: function(data) {
                    $(".modal-title").html("Detalle del Pedido " + orderId);
                    $("#modalbody").html(data);
                    $("#b1").html("Cerrar");
                    $("#b2").hide();
                    
                    $("#modal").modal("show"); 
                },
                error: function(xhr, textStatus, error) {
                    console.log(textStatus);
                },
                statusCode: {
                    200: function(data) {
                        console.log("Respuesta OK");
                        console.log(data);
                    },
                    404: function() {
                        console.log("Página no encontrada");
                    }
                }
            });
        },
        GetOrderDetails3: function(orderId) {
            // Método .ajax() con promesas de jQuery
            $.ajax({
                type: "POST",
                async: true,
                url: "/pedidos/detalles/" + orderId,
                data: {},
                headers: {},
                contentType: "application/json",
                accepts: "text/html"                              
            }).done(function(data) {
                $(".modal-title").html("Detalle del Pedido " + orderId);
                $("#modalbody").html(data);
                $("#b1").html("Cerrar");
                $("#b2").hide();
                
                $("#modal").modal("show"); 
            }).fail(function(xhr, textStatus, error) {
                console.log(textStatus);
            }).always(function(data, textStatus, error) {
                console.log(textStatus);
            });
        },
        GetOrderDetails4: function(orderId) {
            // Utilizado XMLHttpRequest
            var xhr = new XMLHttpRequest();

            xhr.onreadystatechange = function(e) {
                if(xhr.readyState == 4 && xhr.status == 200)
                {
                    document.querySelector(".modal-title").innerHTML = "Detalle del Pedido " + orderId;
                    document.getElementById("modalbody").innerHTML = xhr.responseText;
                    document.getElementById("b1").innerHTML = "Cerrar";
                    document.getElementById("b2").style.display = "none";
                    
                    var modal = new bootstrap.Modal(document.getElementById("modal"));
                    modal.show();  
                }
            }

            xhr.open("POST", "/pedidos/detalles/" + orderId, true);
            xhr.send();
        },
        GetOrderDetails5: function(orderId) {
            $.post("/pedidos/detalles2/" + orderId, {}, function(data) {
                let table = $("<table>");
                let tbody = $("<tbody>");
                let total = 0;

                table.addClass("table table-striped");

                for(var i in data.$values)
                {
                    let subtotal = data.$values[i]["Quantity"] * data.$values[i]["UnitPrice"];
                    total += subtotal;

                    let trItem = $("<tr>")                    

                    trItem.append("<td>" + data.$values[i]["Product"]["ProductName"] + "</td>");
                    trItem.append("<td style='text-align: right;'>" + data.$values[i]["Quantity"] + "</td>");
                    trItem.append("<td style='text-align: right;'>" + data.$values[i]["UnitPrice"].toFixed(2).replace(".", ",")  + "</td>");
                    trItem.append("<td style='text-align: right;'>" + subtotal.toFixed(2).replace(".", ",") + "</td>");

                    tbody.append(trItem);
                }

                let trItem = $("<tr>");
                trItem.append("<td colspan='3' style='text-align: right;'><b>Importe Total</b></td>");
                trItem.append("<td style='text-align: right;'><b>" + total.toFixed(2).replace(".", ",") + "</b></td>");
                tbody.append(trItem);

                table.append(tbody);

                $(".modal-title").html("Detalle del Pedido " + orderId);

                $("#modalbody").html("");
                $("#modalbody").append(table);

                $("#b1").html("Cerrar");
                $("#b2").hide();
                
                $("#modal").modal("show");  
            });
        },                   
    },
    Data: {},
    Page: {
        Customers: {
            List: {
                OnLoad: function() {
                    App.Core.CreateTable("clientes");
                }
            },
            File: {
                OnLoad: function() {
                    App.Core.CreateTable("pedidos");
                    $("button.btn-detalle").click(function(e) {
                        App.Core.GetOrderDetails($(this).data("orderid"));                        
                    });
                    $("button.btn-detalle2").click(function(e) {
                        App.Core.GetOrderDetails5($(this).data("orderid"));                        
                    });                    
                }
            }
        },
        Orders: {
            List: {
                OnLoad: function() {
                    App.Core.CreateTable("pedidos");                    
                    $("button[data-type='detalle']").click(function(e) {
                        App.Core.GetOrderDetails($(this).data("orderid"));  
                    });
                    $("button[data-type='detalle2']").click(function(e) {
                        App.Core.GetOrderDetails5($(this).data("orderid"));  
                    });                    
                }
            },
            Detail: {
                OnBegin: function() { console.log("OnBegin !!!"); },
                OnSuccess: function(id) {
                    $(".modal-title").html("Detalle del Pedido " + id);
                    $("#b1").html("Cerrar");
                    $("#b2").hide();
                    
                    $("#modal").modal("show");
                },
                OnFailure: function() { console.log("OnFailure !!!"); },
                OnComplete: function() { console.log("OnComplete !!!"); },
            }
        },
        Products: {
            List: {
                OnLoad: function() {
                    App.Core.CreateTable("productos");   
                       
                    $(".text-units").click(function(e){
                        alert("Stock: " + $(this).data("stock") + "\nPedido: " + $(this).data("order"));
                    });  
                    
                    $('[data-bs-toggle="tooltip"]').tooltip();
                }
            }            
        }
    }
}