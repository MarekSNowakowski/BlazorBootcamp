redirectToCheckout = function (sessionId) {
    var stripe = Stripe("pk_test_51PWOBI03Wa4QLooRMOT5ACqEqLv8IKy0roTuiZmTnAbzl7p2WHsuhMhZqtCeahPNVC7pzJsqGvTpYwpp9skbVNWs00TssRbYzG");
    stripe.redirectToCheckout({ sessionId: sessionId });
}