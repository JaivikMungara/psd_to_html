function upcoming_service(evt, service) {

	var i, tabcontent, tablinks;
	tabcontent = document.getElementsByClassName("tab-contant");
	for (i = 0; i < tabcontent.length; i++) {
		tabcontent[i].style.display = "none";
	}

	tablinks = document.getElementsByClassName("tablinks");


	for (i = 0; i < tablinks.length; i++) {
		tablinks[i].className = tablinks[i].className.replace(" active", "");
	}


	document.getElementById(service).style.display = "block";
	evt.currentTarget.className += " active";
}


var vTabId = "dashboardTabBtn"; var url1 = new URLSearchParams(window.location.search);
var urlcust = url1.toString();

if (urlcust.includes("=")) {
	var indexofequl = urlcust.lastIndexOf("=");
	vTabId = urlcust.substring(indexofequl + 1, urlcust.length);
}

document.getElementById(vTabId).click(); $("#dashbordTable").click(function (e) {
	serviceRequestId = e.target.closest("tr").getAttribute("data-value");
	alert(serviceRequestId);
	if (e.target.classList == "customerReschedule") {

		document.getElementById("updateRequestId").value = e.target.value;
		alert("In if");
	}
	if (e.target.classList == "customerCancel") {
		document.getElementById("CancelRequestId").value = e.target.value;
	}
	if (serviceRequestId != null && (e.target.classList != "customerCancel" && e.target.classList != "customerReschedule")) {
		document.getElementById("serviceReqdetailsbtn").click();
	}



	console.log(e);
}); document.getElementById("RescheduleServiceRequest").addEventListener("click", function () {
	var serviceStartDate = document.getElementById("selected_date").value;
	var serviceTime = document.getElementById("selected_time").value;
	var serviceRequestId = document.getElementById("updateRequestId").value;
	console.log(serviceRequestId);
	var data = {};
	data.Date = serviceStartDate;
	data.startTime = serviceTime;
	data.serviceRequestId = serviceRequestId;
	$.ajax({
		type: 'POST', url: '/Customer/RescheduleServiceRequest', contentType: 'application/x-www-form-urlencoded; charset=UTF-8', data: data, success: function (result) {
			if (result.value == "true") {
				window.location.reload();
			}
			else {
				alert("fail");
			}
		},
		error: function () {
			alert("error");
		}
	});
});



document.getElementById("CancelRequestBtn").addEventListener("click", function () {

	var ServiceRequestId = document.getElementById("CancelRequestId").value;
	var Comments = document.getElementById("cancelReason").value;
	var data = {};
	data.serviceRequestId = ServiceRequestId;
	data.comments = Comments;
	$.ajax({
		type: 'POST', url: '/Customer/CancelServiceRequest', contentType: 'application/x-www-form-urlencoded; charset=UTF-8', data: data, success: function (result) {
			if (result.value == "true") {
				window.location.reload();
			}
			else {
				alert("fail");
			}
		},
		error: function () {
			alert("error");
		}
	});

});
