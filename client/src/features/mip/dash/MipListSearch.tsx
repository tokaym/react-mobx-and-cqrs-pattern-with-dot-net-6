import { observer } from "mobx-react-lite";
import { Search } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import "../../mip/dash/mipstyle.css";

export default observer(function MipListSearch() {
    const { mipStore } = useStore();
    const { handleSearch, search} = mipStore;
    return (
        <Search onSearchChange={(e,data) => handleSearch(e,data)} value={search} input={{ icon: 'search', iconPosition: 'left' }} />
    )
})