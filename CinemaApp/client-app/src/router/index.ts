/**
 * router/index.ts
 *
 * Manual routes for ./src/pages/*.vue
 */

// Composables
import { createRouter, createWebHistory } from 'vue-router'
import Movies from '@/pages/Movies.vue'
import Movie from '@/pages/Movie.vue'
import Session from '@/pages/Session.vue'
import NotFound from '@/pages/NotFound.vue'
import Ticket from '@/pages/Ticket.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      component: Movies,
      alias: '/movies',
    },
    {
      path: '/movies/:id',
      component: Movie,
    },
    {
      path: '/sessions/:id',
      component: Session,
    },
    {
      path: '/tickets/:id',
      component: Ticket,
    },
    {
      path: '/:pathMatch(.*)*',
      component: NotFound,
    },
  ],
})

export default router
