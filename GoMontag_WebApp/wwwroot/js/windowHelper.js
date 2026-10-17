let resizeCallback = null;

export function getWindowDimensions() {
    return { width: window.innerWidth, height: window.innerHeight };
}

export function startResizeListener(dotNetHelper) {
    if (resizeCallback) return;

    resizeCallback = () => {
        const dims = getWindowDimensions();
        dotNetHelper.invokeMethodAsync('OnResize', dims.width, dims.height);
    };

    window.addEventListener('resize', resizeCallback);
}

export function stopResizeListener() {
    if (resizeCallback) {
        dinow.removeEventListener('resize', resizeCallback);
        resizeCallback = null;
    }
}