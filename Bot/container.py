from dependency_injector import containers, providers

from services.events import EventsService
from services.users import UsersService


class DiContainer(containers.DeclarativeContainer):
    events_service = providers.Singleton(EventsService)
    users_service = providers.Singleton(UsersService)
