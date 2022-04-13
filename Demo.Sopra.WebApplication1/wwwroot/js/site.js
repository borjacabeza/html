let App = {
    Core: {
        CreateTable: function(id) {
            $("#" + id).DataTable({
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
                }
            });           
        }
    },
    Data: {},
    Page: {
        Customers: {
            List: {
                OnLoad: function() {
                    App.Core.CreateTable("clientes");
                }
            }
        },
        Orders: {
            List: {
                OnLoad: function() {
                    App.Core.CreateTable("pedidos");
                }
            },
            Detail: {
                OnBegin: function() { console.log("OnBegin !!!"); },
                OnSuccess: function() {
                    $(".modal-title").html("Detalle del Pedido");
                    $("#b1").html("Cerrar");
                    $("#b2").hide();
                    
                    $("#modal").modal('show');
                },
                OnFailure: function() { console.log("OnFailure !!!"); },
                OnComplete: function() { console.log("OnComplete !!!"); },
            }
        }
    }
}