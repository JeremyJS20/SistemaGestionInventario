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

let modalTargetsEl = document.querySelectorAll('.modal');

modalTargetsEl.forEach(mEl => {
    const options = {
        closable: true,
        onHide: () => {
            const form = mEl.querySelector('form');

            if (form) {
                Array.from(form.elements).forEach(el => {
                    switch (el.type) {
                        case 'text':
                        case 'password':
                        case 'email':
                        case 'number':
                        case 'url':
                        case 'search':
                        case 'tel':
                        case 'textarea':
                            el.value = '';
                            break;

                        case 'checkbox':
                        case 'radio':
                            el.checked = false;
                            break;

                        case 'select-one':
                        case 'select-multiple':
                            Array.from(el.options).forEach(option => option.selected = false);
                            break;

                        case 'file':
                            el.value = null;
                            break;

                        default:
                            el.value = '';
                    }
                });
            }
        },
    };

    const modal = new Modal(mEl, options);

    document.querySelectorAll(`.${mEl.id}-open-btn`).forEach(btn => {
        btn.addEventListener('click', () => {
            modal.show()
        })
    })

    document.querySelectorAll(`.${mEl.id}-close-btn`).forEach(btn => {
        btn.addEventListener('click', () => {
            modal.hide()
        })
    })
});