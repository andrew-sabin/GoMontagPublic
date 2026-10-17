export function observeComponentSize (element, dotNetHelper) {
    if (!element) return;

    const observer = new ResizeObserver(entries => {
        for (let entry of entries) {
            const rect = entry.contentRect;
            dotNetHelper.invokeMethodAsync('OnComponentResized', rect.width, rect.height);
        }
    });

    observer.observe(element);
    return observer;
};