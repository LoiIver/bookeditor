ko.bindingHandlers.selectPicker = {
	init: function (element, valueAccessor, allBindingsAccessor) {
		if ($(element).is('select')) {
			$(element).addClass('selectpicker').selectpicker();
			if (ko.isObservable(valueAccessor())) {
				if ($(element).prop('multiple') && $.isArray(ko.utils.unwrapObservable(valueAccessor()))) {
					ko.bindingHandlers.selectedOptions.init(element, valueAccessor, allBindingsAccessor);
				} else {
					ko.bindingHandlers.value.init(element, valueAccessor, allBindingsAccessor);
				}
			}
		}
	},
	update: function (element, valueAccessor, allBindingsAccessor) {
		if ($(element).is('select')) {
			var isDisabled = ko.utils.unwrapObservable(allBindingsAccessor().disable);
			if (isDisabled) {
				// the dropdown is disabled and we need to reset it to its first option
				$(element).selectpicker('val', $(element).children('option:last').val());
			}
			// React to options changes
			ko.unwrap(allBindingsAccessor.get('options'));
			// React to value changes
			ko.unwrap(allBindingsAccessor.get('value'));
			// Wait a tick to refresh
			setTimeout(() => { $(element).selectpicker('refresh'); }, 0);
		}
	}
};
