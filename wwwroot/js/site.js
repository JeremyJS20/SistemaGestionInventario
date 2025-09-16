// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//sidebar code
const isCollapsed = localStorage.getItem('isSidebarCollapsed') === 'true';
if (isCollapsed) document.getElementById('sidebar-wrapper').classList.add('collapsed');

function toggleCollapseSidenav() {
    const sideBarWrapper = document.getElementById('sidebar-wrapper');
    if (sideBarWrapper.className.includes('collapsed')) {
        sideBarWrapper.classList.remove('collapsed');
        localStorage.removeItem('isSidebarCollapsed');
        localStorage.setItem('isSidebarCollapsed', false)
        document.cookie = "sidebarCollapsed=false; path=/";
    } else {
        sideBarWrapper.classList.add('collapsed');
        localStorage.setItem('isSidebarCollapsed', true);
        document.cookie = "sidebarCollapsed=true; path=/";
    }
}

function toggleSidenavCollapsibleItem(number) {
    const sidenavCollapsibleItem = document.getElementById(`collapsible-sidebar-item-${number}`);

    const sidenavCollapsibleItemName = `isSidenavCollapsibleItem${number}Collapsed`

    if (sidenavCollapsibleItem.className.includes('collapsed')) {
        sidenavCollapsibleItem.classList.remove('collapsed');
        localStorage.removeItem(sidenavCollapsibleItemName);
        localStorage.setItem(sidenavCollapsibleItemName, false)
        document.cookie = `${sidenavCollapsibleItemName}=false; path=/`;
    } else {
        sidenavCollapsibleItem.classList.add('collapsed');
        localStorage.setItem(sidenavCollapsibleItemName, true);
        document.cookie = `${sidenavCollapsibleItemName}=true; path=/`;
    }
}