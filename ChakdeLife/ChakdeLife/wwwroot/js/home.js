// ---- Intersection Observer for scroll-in animations ----
const observerOptions = {
    root: null,
    threshold: 0.12,
    rootMargin: '0px 0px -40px 0px'
};

const observer = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.style.opacity = '1';
            entry.target.style.transform = 'translateY(0)';
            observer.unobserve(entry.target);
        }
    });
}, observerOptions);

// Animate product cards, stat numbers, and section headers on scroll
document.querySelectorAll('.product-card, .stat__number, .section__title, .cat-banner, .ugc-cell').forEach((el, i) => {
    el.style.opacity = '0';
    el.style.transform = 'translateY(28px)';
    el.style.transition = `opacity 0.55s ease ${i * 0.04}s, transform 0.55s ease ${i * 0.04}s`;
    observer.observe(el);
});

// ---- Wishlist toggle ----
document.querySelectorAll('.product-card__wish').forEach(btn => {
    btn.addEventListener('click', (e) => {
        e.preventDefault();
        const svg = btn.querySelector('svg path');
        const active = btn.dataset.active === 'true';
        btn.dataset.active = !active;
        svg.style.fill = !active ? '#c0392b' : 'none';
        svg.style.stroke = !active ? '#c0392b' : 'var(--black)';
    });
});

// ---- Color swatch switcher ----
document.querySelectorAll('.product-card__swatches').forEach(group => {
    group.querySelectorAll('.swatch').forEach(sw => {
        sw.addEventListener('click', () => {
            group.querySelectorAll('.swatch').forEach(s => s.classList.remove('active'));
            sw.classList.add('active');
        });
    });
});
