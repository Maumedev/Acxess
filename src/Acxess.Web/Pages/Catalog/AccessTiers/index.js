document.addEventListener('alpine:init', () => {


    Alpine.data('accessTiersApp', (initialData) => {
        return {
            items: initialData || [],
            search: '',
            selectedId: null,
            isDirty: false,
            loading: false,
            form: {
                idAccessTier: null,
                name: '',
                description: '',
                isActive: true
            },
            get filteredAccessTiers() {
                if (this.search === '') return this.items;
                return this.items.filter(p => p.name.toLowerCase().includes(this.search.toLowerCase()));
            },
            createNew() {
                this.selectedId = 0;

                this.form = {
                    idAccessTier: 0,
                    name: '',
                    description: '',
                    isActive: true
                };

                this.isDirty = true;

                this.$nextTick(() => document.getElementById('Input_Name').focus());
            },
            selectItem(item) {
                this.selectedId = item.idAccessTier;
                this.form = JSON.parse(JSON.stringify(item)); 
                this.isDirty = false;
            }, 
            resetForm() {
                this.selectedId = null;
                this.form.idAccessTier = null;
            },
            init(){
                document.body.addEventListener('listUpdated', (evt) => {

                    this.items = [];
                    this.items = evt.detail.value;
                    if (this.form.idAccessTier > 0) {
                        const updatedItem = this.items.find(x => x.idAccessTier === this.form.idAccessTier);
                        
                        if (updatedItem) {
                            this.selectItem(updatedItem);
                            this.$nextTick(() => this.isDirty = false);
                        } else {
                            this.resetForm();
                        }
                    } else {
                        this.resetForm();
                    }
                });

                this.$watch('form', () => {
                    if(this.selectedId !== null) this.isDirty = false;
                });
            }
        }
    })
})