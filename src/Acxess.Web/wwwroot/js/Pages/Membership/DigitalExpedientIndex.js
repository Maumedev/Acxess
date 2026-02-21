document.addEventListener('alpine:init', () => {
    Alpine.data('digitalExpedientApp', (initialId) => ({
        selectedMemberId: initialId,
        selectMember(id) {
            this.selectedMemberId = id;
        }
    }))
});