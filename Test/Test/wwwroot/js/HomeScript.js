// Hero Section Functionality
document.addEventListener('DOMContentLoaded', function () {
    // Scroll indicator dots functionality
    const dots = document.querySelectorAll('.dot');
    const heroSection = document.querySelector('.hero');

    // Change background when dots are clicked
    dots.forEach((dot, index) => {
        dot.addEventListener('click', () => {
            // Remove active class from all dots
            dots.forEach(d => d.classList.remove('active'));

            // Add active class to clicked dot
            dot.classList.add('active');

            // Change hero background based on clicked dot
            changeHeroBackground(index);
        });
    });

    // Function to change hero background
    function changeHeroBackground(index) {
        // Array of background images for rotation
        const backgrounds = [
            '/Images/HomePageImage1.jpg',
            'https://images.unsplash.com/photo-1511578314322-379afb476865?ixlib=rb-4.0.3&auto=format&fit=crop&w=2069&q=80',
            'https://images.unsplash.com/photo-1497366754035-f200968a6e72?ixlib=rb-4.0.3&auto=format&fit=crop&w=2069&q=80'
        ];

        // Fade out effect
        heroSection.style.opacity = '0.7';
        heroSection.style.transition = 'opacity 0.5s ease';

        setTimeout(() => {
            // Change background image
            heroSection.style.backgroundImage = `url('${backgrounds[index]}')`;
            heroSection.style.opacity = '1';
        }, 300);
    }

    // Optional: Auto-rotate background images
    let currentSlide = 0;
    const slideInterval = 8000; // 8 seconds

    function autoRotateSlides() {
        setInterval(() => {
            currentSlide = (currentSlide + 1) % dots.length;

            // Update dots
            dots.forEach((dot, index) => {
                dot.classList.toggle('active', index === currentSlide);
            });

            // Change background
            changeHeroBackground(currentSlide);
        }, slideInterval);
    }

    // Uncomment the line below to enable auto-rotation
    // autoRotateSlides();

    // Add hover effects to buttons
    const heroButtons = document.querySelectorAll('.hero-buttons .nav-link');

    heroButtons.forEach(button => {
        button.addEventListener('mouseenter', function () {
            this.style.transform = 'translateY(-3px)';
        });

        button.addEventListener('mouseleave', function () {
            this.style.transform = 'translateY(0)';
        });
    });

    // Parallax effect on scroll (optional)
    window.addEventListener('scroll', () => {
        const scrolled = window.pageYOffset;
        const rate = scrolled * 0.5;

        // Apply parallax effect to hero background
        heroSection.style.backgroundPosition = `center ${rate}px`;

        // Fade out hero content on scroll
        const heroContent = document.querySelector('.hero-content');
        const opacity = 1 - (scrolled / 500);
        heroContent.style.opacity = Math.max(opacity, 0.3).toString();
    });

    // Typewriter effect for hero title (optional)
    function typeWriterEffect() {
        const heroTitle = document.querySelector('.hero-title');
        const text = heroTitle.textContent;
        heroTitle.textContent = '';

        let i = 0;
        function type() {
            if (i < text.length) {
                heroTitle.textContent += text.charAt(i);
                i++;
                setTimeout(type, 50);
            }
        }

        // Uncomment to enable typewriter effect
        // setTimeout(type, 1000);
    }

    // Call typewriter effect on page load
    // typeWriterEffect();
});

// Additional animations for page load
window.addEventListener('load', function () {
    const heroElements = document.querySelectorAll('.hero-tagline, .hero-title, .hero-description, .hero-buttons');

    heroElements.forEach((element, index) => {
        element.style.opacity = '0';
        element.style.transform = 'translateY(20px)';
        element.style.transition = `opacity 0.8s ease ${index * 0.2}s, transform 0.8s ease ${index * 0.2}s`;

        setTimeout(() => {
            element.style.opacity = '1';
            element.style.transform = 'translateY(0)';
        }, 100 + (index * 200));
    });
});