Vue.directive('date', {
    bind: function (el, binding, vnode) {        
        el.addEventListener('change', function () {
            // Give focus to the textbox to ensure validation fires immediately.
            el.focus();
        });
    },
    inserted: function (el, binding, vnode) {
        // If not touch enabled browser that supports date inputs.
        if (!Modernizr.touch || !Modernizr.inputtypes.date) {
            // Change the type to text to prevent native date control from appearing.
            el.type = 'text';
            el.setAttribute("data-vv-validate-on", "change");
            // Initialise the Materliaze datepicker control.
            M.Datepicker.init(el, {
                format: 'dd/mm/yyyy',
                autoClose: true
            });
        }
    }
})