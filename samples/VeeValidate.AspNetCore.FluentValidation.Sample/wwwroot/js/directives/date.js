Vue.directive('date', {
    inserted(el, binding, vnode) {
        el.type = "text";
        // Initialise the Materliaze datepicker control.
        M.Datepicker.init(el, {
            format: 'dd/mm/yyyy',
            autoClose: true
        });
    },
    unbind(el) {
        if (el.type === 'text') {
            var instance = M.Datepicker.getInstance(el);
            instance.destroy();
        }
    }
});