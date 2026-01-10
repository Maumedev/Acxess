document.addEventListener('alpine:init', () => {
    Alpine.data('sellingPlanApp', (initialData, accesTiersData) => {


        return {
            items: initialData || [],
            accessTiers : accesTiersData || [],
            search: '',
            form:{
                idSellingPlan: null,
                name: '',
                totalMembers: 1,
                durationInValue: 1,
                durationUnit: 2,
                price: 0.0,
                isActive: true,
                accessTiersIds : [],
            },
            selectedId: null,
            isDirty: false,
            get filteredPlans() {
                if (this.search === '') return this.items;
                return this.items.filter(p => p.name.toLowerCase().includes(this.search.toLowerCase()));
            },
            toggleAccessTiers(id) {
                if (this.form.accessTiersIds.includes(id)) {
                    this.form.accessTiersIds = this.form.accessTiersIds.filter(x => x !== id);
                } else {
                    this.form.accessTiersIds.push(id);
                }
                this.isDirty = true;
            },
            showAccessTier(){
                if (this.form.accessTiersIds.length > 0) {
                    return this.accessTiers
                        .filter(tier => this.form.accessTiersIds.includes(tier.id))
                        .map(tier => tier.name)
                        .join(', ');
                }

                return '';
            },
            resetForm() {
                this.selectedId = null;
                this.form.idSellingPlan = null;
            },
            mapDayUnit(value){
                const dict = {1: 'Días', 2: 'Meses', 3: 'Años'}
                return dict[value] || 'Desconocido';
            },
            selectItem(item) {
                this.selectedId = item.idSellingPlan;
                this.form = JSON.parse(JSON.stringify(item)); 
                // this.isDirty = false;
            },  
            createNew() {
                this.selectedId = 0;

                this.form = {
                    idSellingPlan: 0,
                    name: '',
                    totalMembers: 1,
                    durationInValue: 1,
                    durationUnit: 2,
                    price: 0.0,
                    isActive: true,
                    accessTiersIds : [],
                };

                this.isDirty = true;

                this.$nextTick(() => document.getElementById('Input_Name').focus());
            },
            init(){
                document.body.addEventListener('listUpdated', (evt) => {

                    console.log('Lista recibida del servidor:', evt.detail);

                    this.items = evt.detail;
                    
                    if (this.form.idSellingPlan > 0) {
                        const updatedItem = this.items.find(x => x.idSellingPlan === this.form.idSellingPlan);
                        
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
            },
            
        }
    })
})