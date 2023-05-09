import { Link } from "react-router-dom"
import { Button, Header, Icon, Segment } from "semantic-ui-react"

export default function NotFound(){
    return (
        <Segment>
            <Header>
                <Icon name="search" />
                Sayfa bulunamadı..
            </Header>
            <Segment.Inline>
                <Button as={Link} to="/home" primary>
                    Ana Sayfaya Dön
                </Button>
            </Segment.Inline>
        </Segment>
    )
}