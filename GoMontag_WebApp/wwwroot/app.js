// Error UI functionality
document.addEventListener('DOMContentLoaded', function () {
    const errorUi = document.getElementById('blazor-error-ui');
    if (errorUi) {
        // Handle dismiss button
        const dismissBtn = errorUi.querySelector('.dismiss');
        if (dismissBtn) {
            dismissBtn.addEventListener('click', function () {
                errorUi.classList.remove('show');
            });
        }

        // Handle reload link
        const reloadBtn = errorUi.querySelector('.reload');
        if (reloadBtn) {
            reloadBtn.addEventListener('click', function (e) {
                errorUi.classList.remove('show');
            });
        }

        // Show error UI when Blazor errors occur
        window.addEventListener('error', function () {
            errorUi.classList.add('show');
        });
    }
});