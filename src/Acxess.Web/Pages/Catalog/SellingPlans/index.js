document.addEventListener('alpine:init', () => {
    Alpine.data('sellingPlanForm', (initialData) => {
        
        
        // state
        return {
            id: initialData.IdSellingPlan,
            name: initialData.Name || '',
            price: initialData.Price || 0,
            totalMembers: initialData.TotalMembers || 1,
            isActive: initialData.IsActive,
            durationVal: initialData.DurationInValue || 1,
            durationUnit: initialData.DurationUnit || 2, // 2 = Meses default
            selectedTiers: initialData.AccessTiersIds || [],
            tiersMap: {}, 
            units: { 1: "Días", 2: "Meses", 3: "Años" },
            init() {
                if (window.availableTiersData) {
                    this.tiersMap = window.availableTiersData.reduce((acc, tier) => {
                        acc[tier.IdAccessTier] = tier.Name;
                        return acc;
                    }, {});
                }
            },
            get formattedTiers() {
                if (this.selectedTiers.length === 0) return "";
                return this.selectedTiers
                    .map(id => this.tiersMap[id])
                    .filter(Boolean)
                    .join(", ");
            },
            get durationLabel() {
                return this.units[this.durationUnit] || 'Unidad';
            }
        }
    })
    
    Alpine.data('sellingPlanApp', (initialData, accesTiersData) => {


        return {
            selectedId: null, 
            isLoading: false,

            mapDayUnit(value){
                const dict = {1: 'Días', 2: 'Meses', 3: 'Años'}
                return dict[value] || 'Desconocido';
            }
            

        }
    })
})