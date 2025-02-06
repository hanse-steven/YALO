const screen = document.getElementById('screen')

export function addHandlers() {
    screen.addEventListener('mousemove', (e) => OnMouseMove(e))
    
    resize((o) => {
        o.addEventListener('mouseup', () => OnMouseUp())
        o.addEventListener('mousedown', (e) => OnMouseDown(e, o.id.toString()))
    })
    
    window.addEventListener('resize', () => resize())
}

export function getPositionDimension() {
    let data = []
    document.querySelectorAll('.rectangle').forEach((o) => {
        data.push({
            Id: parseInt(o.id),
            Name: o.innerText,
            X: parseInt(o.dataset.left),
            Y: parseInt(o.dataset.top),
            Width: parseInt(o.dataset.width),
            Height: parseInt(o.dataset.height),
            Color: o.style.backgroundColor
        })
    })
    return data
}

export function resize(callback) {
    const rapport = screen.offsetWidth / screen.dataset.width
    console.log(screen.offsetWidth)
    console.log(screen.dataset.width)
}

let lastSelectedElement = null
let draggedElement= null
let offsetX = 0
let offsetY = 0

const OnMouseUp = () => {
    if (draggedElement) {
        const left = parseFloat(draggedElement.style.left.replace('px',''))
        const top = parseFloat(draggedElement.style.top.replace('px',''))
        const rapport = (screen.dataset.width / screen.offsetWidth).toFixed(2)
        
        draggedElement.dataset.left = (left * rapport).toFixed(2)
        draggedElement.dataset.top = (top * rapport).toFixed(2)
        draggedElement.style.cursor = 'grab'
        draggedElement = null
    }
}

const OnMouseDown = (event, module) => {
    draggedElement = document.getElementById(module)
    offsetX = event.clientX - draggedElement.offsetLeft
    offsetY = event.clientY - draggedElement.offsetTop
    
    draggedElement.classList.add('isSelected')
    draggedElement.style.cursor = 'grabbing'
    if (lastSelectedElement && lastSelectedElement !== draggedElement) {
        lastSelectedElement.classList.remove('isSelected')
    }
    lastSelectedElement = draggedElement
}

const OnMouseMove = (event) =>  {
    if (draggedElement) {
        const screen = document.getElementById('screen')
        const screenRect = screen.getBoundingClientRect()

        let newLeft = event.clientX - offsetX;
        let newTop = event.clientY - offsetY;

        newLeft = Math.max(0, Math.min(newLeft, screenRect.width - draggedElement.offsetWidth));
        newTop = Math.max(0, Math.min(newTop, screenRect.height - draggedElement.offsetHeight));

        draggedElement.style.left = `${newLeft}px`;
        draggedElement.style.top = `${newTop}px`;
    }
}
