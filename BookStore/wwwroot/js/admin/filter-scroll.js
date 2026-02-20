document.addEventListener("DOMContentLoaded", function () {
    const rightPanel = document.querySelector('.admin-table-right');
    const rightScroll = document.querySelector('.admin-table-filter-scrollable');

    rightPanel.addEventListener('wheel', function (e) {
        const atTop = rightScroll.scrollTop === 0;
        const atBottom = rightScroll.scrollTop + rightScroll.clientHeight >= rightScroll.scrollHeight - 1;
        if (atTop && e.deltaY < 0) e.preventDefault();
        if (atBottom && e.deltaY > 0) e.preventDefault();
    }, { passive: false });
});