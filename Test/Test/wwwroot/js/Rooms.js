document.addEventListener('DOMContentLoaded', function () {
    // Get all room cards
    const roomCards = document.querySelectorAll('.room-card');
    const roomsGrid = document.getElementById('roomsGrid');
    const noResults = document.getElementById('noResults');
    const activeFilters = document.getElementById('activeFilters');
    const filterTags = document.getElementById('filterTags');

    // Store current filters
    let currentFilters = {
        price: { min: null, max: null },
        capacity: { min: null, max: null },
        availability: null,
        search: null,
        sort: { field: null, order: null }
    };

    // Filter buttons
    document.getElementById('showAll').addEventListener('click', () => {
        currentFilters.availability = null;
        applyFilters();
    });

    document.getElementById('showAvailable').addEventListener('click', () => {
        currentFilters.availability = 'available';
        applyFilters();
    });

    document.getElementById('filterPrice').addEventListener('click', () => {
        const minPrice = document.getElementById('minPrice').value;
        const maxPrice = document.getElementById('maxPrice').value;
        currentFilters.price.min = minPrice ? parseFloat(minPrice) : null;
        currentFilters.price.max = maxPrice ? parseFloat(maxPrice) : null;
        applyFilters();
    });

    document.getElementById('filterCapacity').addEventListener('click', () => {
        const minCapacity = document.getElementById('minCapacity').value;
        const maxCapacity = document.getElementById('maxCapacity').value;
        currentFilters.capacity.min = minCapacity ? parseInt(minCapacity) : null;
        currentFilters.capacity.max = maxCapacity ? parseInt(maxCapacity) : null;
        applyFilters();
    });

    document.getElementById('searchRooms').addEventListener('click', () => {
        const searchTerm = document.getElementById('roomSearch').value.toLowerCase();
        currentFilters.search = searchTerm;
        applyFilters();
    });

    // Sort buttons
    document.querySelectorAll('.btn-sort').forEach(button => {
        button.addEventListener('click', function () {
            const field = this.getAttribute('data-sort');
            const order = this.getAttribute('data-order');
            currentFilters.sort.field = field;
            currentFilters.sort.order = order;
            applyFilters();
        });
    });

    // Clear filters
    document.getElementById('clearFilters').addEventListener('click', resetFilters);
    document.getElementById('resetFilters').addEventListener('click', resetFilters);

    // Apply filters function
    function applyFilters() {
        let visibleCount = 0;

        roomCards.forEach(card => {
            let shouldShow = true;

            // Filter by price
            const price = parseFloat(card.getAttribute('data-price'));
            if (currentFilters.price.min !== null && price < currentFilters.price.min) {
                shouldShow = false;
            }
            if (currentFilters.price.max !== null && price > currentFilters.price.max) {
                shouldShow = false;
            }

            // Filter by capacity
            const capacity = parseInt(card.getAttribute('data-capacity'));
            if (currentFilters.capacity.min !== null && capacity < currentFilters.capacity.min) {
                shouldShow = false;
            }
            if (currentFilters.capacity.max !== null && capacity > currentFilters.capacity.max) {
                shouldShow = false;
            }

            // Filter by availability
            if (currentFilters.availability === 'available') {
                const isAvailable = card.getAttribute('data-is-available') === 'true';
                if (!isAvailable) shouldShow = false;
            }

            // Filter by search
            if (currentFilters.search) {
                const name = card.getAttribute('data-name');
                if (!name.includes(currentFilters.search)) {
                    shouldShow = false;
                }
            }

            // Show/hide card
            if (shouldShow) {
                card.style.display = 'block';
                visibleCount++;
            } else {
                card.style.display = 'none';
            }
        });

        // Sort if needed
        if (currentFilters.sort.field && currentFilters.sort.order) {
            sortRooms();
        }

        // Update UI based on results
        updateResultsUI(visibleCount);
        updateActiveFiltersDisplay();
    }

    // Sort rooms function
    function sortRooms() {
        const visibleCards = Array.from(roomCards).filter(card => card.style.display !== 'none');

        visibleCards.sort((a, b) => {
            const aValue = parseFloat(a.getAttribute('data-' + currentFilters.sort.field));
            const bValue = parseFloat(b.getAttribute('data-' + currentFilters.sort.field));

            if (currentFilters.sort.order === 'asc') {
                return aValue - bValue;
            } else {
                return bValue - aValue;
            }
        });

        // Reorder DOM
        visibleCards.forEach(card => {
            roomsGrid.appendChild(card);
        });
    }

    // Update UI based on results
    function updateResultsUI(visibleCount) {
        if (visibleCount === 0) {
            roomsGrid.style.display = 'none';
            noResults.style.display = 'block';
        } else {
            roomsGrid.style.display = 'grid';
            noResults.style.display = 'none';
        }
    }

    // Update active filters display
    function updateActiveFiltersDisplay() {
        filterTags.innerHTML = '';
        let hasActiveFilters = false;

        // Price filter tag
        if (currentFilters.price.min !== null || currentFilters.price.max !== null) {
            const priceText = `Price: £${currentFilters.price.min || '0'} - £${currentFilters.price.max || '∞'}`;
            addFilterTag('price', priceText);
            hasActiveFilters = true;
        }

        // Capacity filter tag
        if (currentFilters.capacity.min !== null || currentFilters.capacity.max !== null) {
            const capacityText = `Capacity: ${currentFilters.capacity.min || '0'} - ${currentFilters.capacity.max || '∞'} people`;
            addFilterTag('capacity', capacityText);
            hasActiveFilters = true;
        }

        // Availability filter tag
        if (currentFilters.availability === 'available') {
            addFilterTag('availability', 'Available Only');
            hasActiveFilters = true;
        }

        // Search filter tag
        if (currentFilters.search) {
            addFilterTag('search', `Search: "${currentFilters.search}"`);
            hasActiveFilters = true;
        }

        // Sort filter tag
        if (currentFilters.sort.field) {
            const fieldName = currentFilters.sort.field === 'starRating' ? 'Star Rating' : 'Guest Rating';
            const orderName = currentFilters.sort.order === 'asc' ? 'Low to High' : 'High to Low';
            addFilterTag('sort', `Sorted by: ${fieldName} (${orderName})`);
            hasActiveFilters = true;
        }

        // Show/hide active filters section
        if (hasActiveFilters) {
            activeFilters.style.display = 'block';
        } else {
            activeFilters.style.display = 'none';
        }
    }

    // Add a filter tag
    function addFilterTag(type, text) {
        const tag = document.createElement('div');
        tag.className = 'filter-tag';
        tag.innerHTML = `
            <span>${text}</span>
            <button class="remove-tag" data-type="${type}">
                <i class="fas fa-times"></i>
            </button>
        `;
        filterTags.appendChild(tag);

        // Add click event to remove button
        tag.querySelector('.remove-tag').addEventListener('click', function () {
            const filterType = this.getAttribute('data-type');
            removeFilter(filterType);
        });
    }

    // Remove specific filter
    function removeFilter(type) {
        switch (type) {
            case 'price':
                currentFilters.price.min = null;
                currentFilters.price.max = null;
                document.getElementById('minPrice').value = '';
                document.getElementById('maxPrice').value = '';
                break;
            case 'capacity':
                currentFilters.capacity.min = null;
                currentFilters.capacity.max = null;
                document.getElementById('minCapacity').value = '';
                document.getElementById('maxCapacity').value = '';
                break;
            case 'availability':
                currentFilters.availability = null;
                break;
            case 'search':
                currentFilters.search = null;
                document.getElementById('roomSearch').value = '';
                break;
            case 'sort':
                currentFilters.sort.field = null;
                currentFilters.sort.order = null;
                break;
        }
        applyFilters();
    }

    // Reset all filters
    function resetFilters() {
        // Reset filter values
        currentFilters = {
            price: { min: null, max: null },
            capacity: { min: null, max: null },
            availability: null,
            search: null,
            sort: { field: null, order: null }
        };

        // Clear input fields
        document.getElementById('minPrice').value = '';
        document.getElementById('maxPrice').value = '';
        document.getElementById('minCapacity').value = '';
        document.getElementById('maxCapacity').value = '';
        document.getElementById('roomSearch').value = '';

        // Show all rooms
        roomCards.forEach(card => {
            card.style.display = 'block';
        });

        // Update UI
        roomsGrid.style.display = 'grid';
        noResults.style.display = 'none';
        activeFilters.style.display = 'none';
    }

    // Initialize
    resetFilters();
});