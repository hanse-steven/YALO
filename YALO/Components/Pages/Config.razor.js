export const addHandlers = () => {
    const screen = document.getElementById('screen')
    screen.addEventListener('mousemove', (e) => OnMouseMove(e))
    
    new ResizeObserver(() => resize()).observe(screen)
    
    window.addEventListener('resize', () => resize())

    resize()
}

export const addListenerRectangle = () => {
    document.querySelectorAll('.rectangle').forEach((o) => {
        o.addEventListener('mouseup', () => OnMouseUp())
        o.addEventListener('mousedown', (e) => OnMouseDown(e, o.id.toString()))
    })
}

export const getPositionDimension = () => {
    let data = []
    document.querySelectorAll('.rectangle').forEach((o) => {
        data.push({
            Id: o.id,
            X: parseInt(o.dataset.left),
            Y: parseInt(o.dataset.top),
            Width: parseInt(o.dataset.width),
            Height: parseInt(o.dataset.height),
        })
    })
    return data
}

export const resize = () => {
    const screen = document.getElementById('screen')
    const rapport = screen.offsetWidth / screen.dataset.width
    
    document.querySelectorAll('.rectangle').forEach((o) => {
        o.style.width = `${o.dataset.width*rapport}px`
        o.style.height = `${o.dataset.height*rapport}px`
        o.style.left = `${o.dataset.left*rapport}px`
        o.style.top = `${o.dataset.top*rapport}px`
    })
}

export const unselectItem = () => {
    if (lastSelectedElement) {
        lastSelectedElement.classList.remove('isSelected')
        lastSelectedElement = null
    }
}

let lastSelectedElement = null
let draggedElement= null
let offsetX = 0
let offsetY = 0

const OnMouseUp = () => {
    if (draggedElement) {
        const left = parseFloat(draggedElement.style.left.replace('px',''))
        const top = parseFloat(draggedElement.style.top.replace('px',''))
        const screen = document.getElementById('screen')
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
        const screenRect = document.getElementById('screen').getBoundingClientRect()

        let newLeft = event.clientX - offsetX;
        let newTop = event.clientY - offsetY;

        newLeft = Math.max(0, Math.min(newLeft, screenRect.width - draggedElement.offsetWidth));
        newTop = Math.max(0, Math.min(newTop, screenRect.height - draggedElement.offsetHeight));

        draggedElement.style.left = `${newLeft}px`;
        draggedElement.style.top = `${newTop}px`;
    }
}
