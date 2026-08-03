(() => {
    // Auto-logout after inactivity (10 minutes)
    const TIMEOUT_MS = 10 * 60 * 1000; // 10 minutes
    let timer = null;

    function resetTimer() {
        if (timer) clearTimeout(timer);
        timer = setTimeout(onTimeout, TIMEOUT_MS);
    }

    async function onTimeout() {
        try {
            // call server logout endpoint to remove HttpOnly cookie
            await fetch('/api/admin/logout', { method: 'POST', credentials: 'same-origin' });
        } catch (e) {
            // ignore network errors
        }
        // redirect to login page
        window.location.href = '/verwaltung';
    }

    // Reset timer on common user interactions
    ['mousemove', 'mousedown', 'keypress', 'touchstart', 'scroll', 'click'].forEach(evt => {
        window.addEventListener(evt, resetTimer, { passive: true });
    });

    // Start initial timer
    resetTimer();
})();
