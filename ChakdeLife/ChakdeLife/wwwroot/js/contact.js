// ---- Scroll reveal ----
const revealEls = document.querySelectorAll('.reveal');
const revealObserver = new IntersectionObserver((entries) => {
    entries.forEach((entry) => {
        if (entry.isIntersecting) {
            const siblings = [...entry.target.parentElement.querySelectorAll('.reveal')];
            const idx = siblings.indexOf(entry.target);
            entry.target.style.transitionDelay = `${idx * 0.07}s`;
            entry.target.classList.add('visible');
            revealObserver.unobserve(entry.target);
        }
    });
}, { threshold: 0.1, rootMargin: '0px 0px -20px 0px' });
revealEls.forEach(el => revealObserver.observe(el));

// ---- Topic pill selector ----
const pills = document.querySelectorAll('.topic-pill');
const hiddenTopic = document.getElementById('selectedTopic');
pills.forEach(pill => {
    pill.addEventListener('click', () => {
        pills.forEach(p => p.classList.remove('active'));
        pill.classList.add('active');
        hiddenTopic.value = pill.dataset.topic;
    });
});

// ---- Character counter ----
const messageEl = document.getElementById('message');
const charCountEl = document.getElementById('charCount');
messageEl.addEventListener('input', () => {
    charCountEl.textContent = messageEl.value.length;
});

// ---- Form submit ----
document.getElementById('contactForm').addEventListener('submit', function (e) {
    e.preventDefault();
    const form = this;
    const btn = form.querySelector('.btn-submit');

    // Basic validation
    if (!form.checkValidity()) {
        form.reportValidity();
        return;
    }

    btn.textContent = 'Sending…';
    btn.disabled = true;

    // Simulate send (replace with actual fetch to your controller)
    setTimeout(() => {
        document.getElementById('contactForm').style.display = 'none';
        document.getElementById('formSuccess').style.display = 'block';
    }, 1200);

    /*
    // Real implementation — uncomment and wire up your controller:
    fetch('/Contact/Send', {
        method: 'POST',
        body: new FormData(form),
        headers: { 'RequestVerificationToken': document.querySelector('[name="__RequestVerificationToken"]').value }
    })
    .then(res => res.json())
    .then(data => {
        if (data.success) {
            document.getElementById('contactForm').style.display = 'none';
            document.getElementById('formSuccess').style.display = 'block';
        }
    })
    .catch(() => {
        btn.textContent = 'Send Message';
        btn.disabled = false;
    });
    */
});

function resetForm() {
    document.getElementById('contactForm').reset();
    document.getElementById('contactForm').style.display = 'flex';
    document.getElementById('formSuccess').style.display = 'none';
    charCountEl.textContent = '0';
    pills.forEach(p => p.classList.remove('active'));
    pills[0].classList.add('active');
    hiddenTopic.value = 'General';
    document.querySelector('.btn-submit').disabled = false;
    document.querySelector('.btn-submit').innerHTML = 'Send Message <svg viewBox="0 0 24 24" style="width:16px;height:16px;stroke:currentColor;fill:none;stroke-width:1.8;stroke-linecap:round"><line x1="5" y1="12" x2="19" y2="12"/><polyline points="12 5 19 12 12 19"/></svg>';
}

// ---- FAQ accordion ----
document.querySelectorAll('.faq-item__trigger').forEach(trigger => {
    trigger.addEventListener('click', () => {
        const item = trigger.closest('.faq-item');
        const isOpen = item.classList.contains('open');

        // Close all
        document.querySelectorAll('.faq-item').forEach(i => {
            i.classList.remove('open');
            i.querySelector('.faq-item__trigger').setAttribute('aria-expanded', 'false');
        });

        // Open clicked if it was closed
        if (!isOpen) {
            item.classList.add('open');
            trigger.setAttribute('aria-expanded', 'true');
        }
    });
});
