$(function () {
	$(".d-none").hide().removeClass("d-none");

	$(".whatiscovered,.whatiscoverednot,.whatiscoveredchart").on(
		"click select",
		(event) => {
			preventEvent(event);

			let target = $(event.target).attr("class").match(/whatiscovered(not)?(chart)?/g)[0];
			$(["#whatiscovered", "#whatiscoverednot", "#whatiscoveredchart"].filter((item) => item !== "#" + target).join(",")).modal("hide");
			$("#" + target).modal("show");
		});
});

//#region General Helper Methods
function preventEvent(event) {
	event.stopPropagation();
	event.preventDefault();
}

function ifNaN(value, alternative) {
	if (isNaN(value)) {
		return alternative;
	}
	else {
		return value;
	}
}

function isNull(value) {
	return typeof (value) === "undefined" || value === null;
}

function pmt(startingValue, periods, rate) {
	let pRate = rate / 12;
	return (pRate * startingValue) / (1 - Math.pow(1 + pRate, -periods));
}

function formatPercent(percent) {
	return (percent * 100).toFixed(0) + "%";
}

/**
 * Generalized form submission method.
 *
 * @param {any} params - Full options object.
 *	Required properties:
 *		form - The form object to be serialized for the request.
 *	Optional properties:
 *		url - Destination URL for the request.
 *		pendingFunction - Function to be executed immediately when the request is pending.
 *		completeFunction - Function to be executed when the request is done.
 *		successFunction - Function to be executed when the request successfully returns.
 *		failedFunction - Function to be executed when the server returns an error.
 */
function formSubmit(params) {
	if (typeof params.form === "undefined") {
		throw "No form specified!";
	}

	params.url = typeof params.url !== "undefined" ? params.url : window.location;
	params.pendingFunction = typeof params.pendingFunction !== "undefined" ? params.pendingFunction : null;
	params.completeFunction = typeof params.completeFunction !== "undefined" ? params.completeFunction : null;
	params.successFunction = typeof params.successFunction !== "undefined" ? params.successFunction : null;
	params.failedFunction = typeof params.failedFunction !== "undefined" ? params.failedFunction : null;
	params.formType = typeof params.formType !== "undefined" ? params.formType : "JSON";

	let data;
	if (params.formType === "JSON") {
		data = {};
		$(params.form).serializeArray().map(function (x) {
			data[x.name] = x.value;
		});
		data = JSON.stringify(data);
	}
	else {
		data = new FormData(params.form[0]);
	}

	setTimeout(params.pendingFunction, 0);

	$.ajax({
		type: "post",
		url: params.url,
		data: data,
		processData: false,
		beforeSend: function () {
			if (typeof params.pendingFunction === "function") {
				setTimeout(function () { params.pendingFunction; }, 0);
			}
		},
		success: function (data) {
			if (typeof params.successFunction === "function") {
				setTimeout(function () { params.successFunction(data); }, 0);
			}
		},
		contentType: params.formType === "JSON" ? "application/json" : false
	})
	.done(function (data) {
		if (typeof params.completeFunction === "function") {
			setTimeout(function () { params.completeFunction(data); }, 0);
		}
	})
	.fail(function (xhr, status, error) {
		if (typeof params.failedFunction === "function") {
			setTimeout(function () { params.failedFunction(xhr, status, error) }, 0);
		}
	});
}
