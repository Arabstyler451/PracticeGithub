// Minimal fix for home page issue
document.addEventListener('DOMContentLoaded', function () {
    const currentPath = window.location.pathname.toLowerCase();

    // Get all main nav links
    const navLinks = document.querySelectorAll('.navbar-nav:not(.ms-auto) .nav-link');

    // Remove active from all
    navLinks.forEach(link => link.classList.remove('active'));

    // Check each link
    navLinks.forEach(link => {
        const href = link.getAttribute('href');
        if (href) {
            const linkPath = href.toLowerCase().replace('~', '');

            // Special handling for home page
            if (currentPath === '/' || currentPath === '/home' || currentPath === '/home/index') {
                // Only activate if link is specifically a home link
                if (linkPath === '/' || linkPath === '/home/index' || linkPath.includes('home/index')) {
                    link.classList.add('active');
                }
            }
            // For other pages
            else if (currentPath.includes(linkPath) && linkPath !== '/') {
                link.classList.add('active');
            }
        }
    });

    // Keep auth links black
    document.querySelectorAll('.navbar-nav.ms-auto .nav-link').forEach(link => {
        link.classList.remove('active');
    });
});